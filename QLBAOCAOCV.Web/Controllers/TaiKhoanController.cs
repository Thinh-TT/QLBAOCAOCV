using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.DAL.Entities;
using QLBAOCAOCV.Web.Filters;

namespace QLBAOCAOCV.Web.Controllers
{
    [AuthFilter]
    [AdminOnlyFilter]
    public class TaiKhoanController : Controller
    {
        private readonly ITaiKhoanService _service;
        private readonly AppDbContext _context;

        public TaiKhoanController(
            ITaiKhoanService service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _service.GetAll();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.NhanViens = new SelectList(
                _context.NhanViens
                    .Where(nv => nv.TaiKhoan == null),
                "MaNV", "TenNV");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int maNV, string username, string role)
        {
            var taiKhoan = new TaiKhoan
            {
                Username = username,
                Role = role,
                MaNV = maNV
            };

            _service.Create(taiKhoan, "123456");

            TempData["Success"] = "Tao tai khoan thanh cong (MK mac dinh: 123456)";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleActive(int id)
        {
            _service.ToggleActive(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ResetPassword(int id)
        {
            _service.ResetPassword(id, "123456");
            TempData["Success"] = "Reset mat khau ve 123456";
            return RedirectToAction(nameof(Index));
        }
    }
}
