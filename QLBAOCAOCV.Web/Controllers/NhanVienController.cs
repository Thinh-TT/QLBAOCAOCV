using Microsoft.AspNetCore.Mvc;
using QLBAOCAOCV.DAL;
using QLBAOCAOCV.DAL.Entities;
using QLBAOCAOCV.Web.Filters;

namespace QLBAOCAOCV.Web.Controllers
{
    [AuthFilter]
    public class NhanVienController : Controller
    {
        private readonly AppDbContext _context;

        public NhanVienController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /NhanVien
        public IActionResult Index()
        {
            var list = _context.NhanViens.ToList();
            return View(list);
        }

        // GET: /NhanVien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                _context.NhanViens.Add(nv);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(nv);
        }

        // GET: /NhanVien/Edit/5
        public IActionResult Edit(int id)
        {
            var nv = _context.NhanViens.Find(id);
            if (nv == null) return NotFound();
            return View(nv);
        }

        // POST: /NhanVien/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                _context.NhanViens.Update(nv);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(nv);
        }

        // GET: /NhanVien/Delete/5
        public IActionResult Delete(int id)
        {
            var nv = _context.NhanViens.Find(id);
            if (nv == null) return NotFound();

            _context.NhanViens.Remove(nv);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
