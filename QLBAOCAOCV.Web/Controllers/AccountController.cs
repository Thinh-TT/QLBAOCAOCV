using Microsoft.AspNetCore.Mvc;
using QLBAOCAOCV.BLL.Services;
using QLBAOCAOCV.Web.ViewModels;

namespace QLBAOCAOCV.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _authService.Login(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Sai tai khoan hoac mat khau");
                return View(model);
            }

            // Luu session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetInt32("MaNV", user.MaNV);
            HttpContext.Session.SetString("TenNV", user.NhanVien.TenNV);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
