using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.DAL.Entities;
using ClosedXML.Excel;
using System.IO;

namespace QLBAOCAOCV.Web.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoService _baoCaoService;
        private readonly AppDbContext _context;

        public BaoCaoController(
            IBaoCaoService baoCaoService,
            AppDbContext context)
        {
            _baoCaoService = baoCaoService;
            _context = context;
        }

        // ===================== INDEX =====================
        public IActionResult Index(
            DateTime? fromDate,
            DateTime? toDate,
            int? maNV,
            int? maPhong)
        {
            var danhSach = _baoCaoService.Search(fromDate, toDate, maNV, maPhong);

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.MaNV = maNV;
            ViewBag.MaPhong = maPhong;

            ViewBag.NhanViens = new SelectList(_context.NhanViens, "MaNV", "TenNV");
            ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");

            return View(danhSach);
        }

        // ===================== CREATE =====================
        public IActionResult Create()
        {
            ViewBag.NhanViens = new SelectList(_context.NhanViens, "MaNV", "TenNV");
            ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BaoCao baoCao)
        {
            if (ModelState.IsValid)
            {
                _baoCaoService.Create(baoCao);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.NhanViens = new SelectList(_context.NhanViens, "MaNV", "TenNV");
            ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");
            return View(baoCao);
        }

        // ===================== XÁC NHẬN =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhan(int id)
        {
            var bc = _baoCaoService.GetById(id);
            if (bc == null || bc.TrangThai != null)
                return RedirectToAction(nameof(Index));

            _baoCaoService.XacNhanBaoCao(id);
            return RedirectToAction(nameof(Index));
        }

        // ===================== EXPORT EXCEL =====================
        public IActionResult ExportExcel(
            DateTime? fromDate,
            DateTime? toDate,
            int? maNV,
            int? maPhong)
        {
            var data = _baoCaoService.Search(fromDate, toDate, maNV, maPhong);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("BaoCao");

            // Header
            ws.Cell(1, 1).Value = "Ngay bao cao";
            ws.Cell(1, 2).Value = "Nhan vien";
            ws.Cell(1, 3).Value = "Phong";
            ws.Cell(1, 4).Value = "Nhiet do";
            ws.Cell(1, 5).Value = "Do am";
            ws.Cell(1, 6).Value = "Trang thai";
            ws.Cell(1, 7).Value = "Ngay xac nhan";
            ws.Cell(1, 8).Value = "Ghi chu";

            int row = 2;
            foreach (var bc in data)
            {
                ws.Cell(row, 1).Value = bc.NgayBC.ToString("dd/MM/yyyy HH:mm");
                ws.Cell(row, 2).Value = bc.NhanVien?.TenNV;
                ws.Cell(row, 3).Value = bc.Phong?.TenPhong;
                ws.Cell(row, 4).Value = bc.NhietDo;
                ws.Cell(row, 5).Value = bc.DoAm;

                // TRẠNG THÁI
                ws.Cell(row, 6).Value = bc.TrangThai switch
                {
                    null => "Chua xac nhan",
                    0 => "Fail",
                    1 => "Pass",
                    _ => "Khong xac dinh"
                };

                ws.Cell(row, 7).Value = bc.NgayXacNhan?.ToString("dd/MM/yyyy HH:mm") ?? "";
                ws.Cell(row, 8).Value = bc.GhiChuBC ?? "";

                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "BaoCaoNhietDoDoAm.xlsx");
        }
    }
}
