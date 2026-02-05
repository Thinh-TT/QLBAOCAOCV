using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLBAOCAOCV.DAL.Entities;


namespace QLBAOCAOCV.DAL.Repositories
{
    public class BaoCaoRepository : IBaoCaoRepository
    {
        private readonly AppDbContext _context;

        public BaoCaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<BaoCao> GetAll()
        {
            return _context.BaoCaos
                .Include(b => b.NhanVien)
                .Include(b => b.Phong);
        }

        public BaoCao? GetById(int id)
        {
            return _context.BaoCaos
                .Include(b => b.NhanVien)
                .Include(b => b.Phong)
                .FirstOrDefault(b => b.MaBC == id);
        }

        public void Update(BaoCao baoCao)
        {
            _context.BaoCaos.Update(baoCao);
            _context.SaveChanges();
        }
    }
}
