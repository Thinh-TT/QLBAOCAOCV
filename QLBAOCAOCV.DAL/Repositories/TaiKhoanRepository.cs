using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLBAOCAOCV.DAL.Entities;


namespace QLBAOCAOCV.DAL.Repositories
{
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly AppDbContext _context;

        public TaiKhoanRepository(AppDbContext context)
        {
            _context = context;
        }

        public TaiKhoan? GetByUsername(string username)
        {
            return _context.TaiKhoans
                .Include(t => t.NhanVien)
                .FirstOrDefault(t =>
                    t.Username == username &&
                    t.IsActive);
        }

        public TaiKhoan? GetById(int userId)
        {
            return _context.TaiKhoans
                .Include(t => t.NhanVien)
                .FirstOrDefault(t => t.UserId == userId);
        }

        public IEnumerable<TaiKhoan> GetAll()
        {
            return _context.TaiKhoans
                .Include(t => t.NhanVien)
                .AsNoTracking()
                .ToList();
        }


        public void Add(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Add(taiKhoan);
            _context.SaveChanges();
        }

        public void Update(TaiKhoan taiKhoan)
        {
            _context.TaiKhoans.Update(taiKhoan);
            _context.SaveChanges();
        }

    }
}
