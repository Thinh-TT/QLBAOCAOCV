using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.DAL.Entities;
using QLBAOCAOCV.Web.Filters;
using QLBAOCAOCV.Web.ViewModels;
using System.IO;

namespace QLBAOCAOCV.Web.Controllers
{
    [AuthFilter]
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
            int currentMaNV = HttpContext.Session.GetInt32("MaNV")!.Value;
            string role = HttpContext.Session.GetString("Role")!;

            var danhSach = _baoCaoService.Search(
                fromDate,
                toDate,
                maNV,
                maPhong,
                currentMaNV,
                role
            );

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            ViewBag.MaNV = maNV;
            ViewBag.MaPhong = maPhong;

            // CHỈ ADMIN MỚI CÓ LIST NHÂN VIÊN
            if (role == "Admin")
            {
                ViewBag.NhanViens = new SelectList(_context.NhanViens, "MaNV", "TenNV");
            }

            ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");

            return View(danhSach);
        }

        // ===================== CREATE =====================
        public IActionResult Create()
        {
            ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BaoCaoCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Phongs = new SelectList(_context.Phongs, "MaPhong", "TenPhong");
                return View(model);
            }

            int maNV = HttpContext.Session.GetInt32("MaNV")!.Value;

            var baoCao = new BaoCao
            {
                MaNV = maNV, // LAY TU SESSION
                MaPhong = model.MaPhong,
                NhietDo = model.NhietDo,
                DoAm = model.DoAm,
                GhiChuBC = model.GhiChuBC,
                NgayBC = DateTime.Now,
                TrangThai = null
            };

            _baoCaoService.Create(baoCao);
            TempData["Success"] = "Them bao cao thanh cong";

            return RedirectToAction(nameof(Index));
        }

        // ===================== XÁC NHẬN =====================
        [AdminOnlyFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XacNhan(int id)
        {
            // 1. Kiem tra quyen
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                TempData["Error"] = "Ban khong co quyen xac nhan bao cao";
                return RedirectToAction(nameof(Index));
            }

            // 2. Lay bao cao
            var bc = _baoCaoService.GetById(id);
            if (bc == null)
            {
                TempData["Error"] = "Bao cao khong ton tai";
                return RedirectToAction(nameof(Index));
            }

            if (bc.TrangThai != null)
            {
                TempData["Error"] = "Bao cao da duoc xu ly truoc do";
                return RedirectToAction(nameof(Index));
            }

            // 3. Xac nhan
            _baoCaoService.XacNhanBaoCao(id);
            TempData["Success"] = "Xac nhan bao cao thanh cong";

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
            ws.Cell(1, 1).Value = "Report Date";
            ws.Cell(1, 2).Value = "Employee";
            ws.Cell(1, 3).Value = "Room";
            ws.Cell(1, 4).Value = "Temperature";
            ws.Cell(1, 5).Value = "Humidity";
            ws.Cell(1, 6).Value = "Status";
            ws.Cell(1, 7).Value = "Confirmation Date";
            ws.Cell(1, 8).Value = "Notes";

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
                    null => "Pending",
                    0 => "Failed",
                    1 => "Passed",
                    _ => "undefined"
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
                "export.xlsx");
        }
    }
}
