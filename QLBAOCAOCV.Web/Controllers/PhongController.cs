using Microsoft.AspNetCore.Mvc;
using QLBAOCAOCV.DAL;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.Web.Controllers
{
    public class PhongController : Controller
    {
        private readonly AppDbContext _context;

        public PhongController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Phong
        public IActionResult Index()
        {
            var list = _context.Phongs.ToList();
            return View(list);
        }

        // GET: /Phong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Phong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Phongs.Add(phong);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(phong);
        }

        // GET: /Phong/Edit/5
        public IActionResult Edit(int id)
        {
            var phong = _context.Phongs.Find(id);
            if (phong == null) return NotFound();
            return View(phong);
        }

        // POST: /Phong/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Phong phong)
        {
            if (ModelState.IsValid)
            {
                _context.Phongs.Update(phong);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(phong);
        }

        // GET: /Phong/Delete/5
        public IActionResult Delete(int id)
        {
            var phong = _context.Phongs.Find(id);
            if (phong == null) return NotFound();

            _context.Phongs.Remove(phong);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
