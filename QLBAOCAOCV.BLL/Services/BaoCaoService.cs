using Microsoft.EntityFrameworkCore;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.BLL.Services
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly AppDbContext _context;

        public BaoCaoService(AppDbContext context)
        {
            _context = context;
        }

        public List<BaoCao> GetAll()
        {
            return _context.BaoCaos
                .Include(b => b.NhanVien)
                .Include(b => b.Phong)
                .OrderByDescending(b => b.NgayBC)
                .ToList();
        }

        public BaoCao? GetById(int maBC)
        {
            return _context.BaoCaos
                .Include(b => b.NhanVien)
                .Include(b => b.Phong)
                .FirstOrDefault(b => b.MaBC == maBC);
        }

        public void Create(BaoCao baoCao)
        {
            baoCao.NgayBC = DateTime.Now;
            baoCao.TrangThai = null;

            _context.BaoCaos.Add(baoCao);
            _context.SaveChanges();
        }

        public void XacNhanBaoCao(int maBC)
        {
            var baoCao = _context.BaoCaos.FirstOrDefault(b => b.MaBC == maBC);
            if (baoCao == null) return;

            baoCao.NgayXacNhan = DateTime.Now;

            if (baoCao.NhietDo < 15)
            {
                SetFail(baoCao, "Nhiệt độ quá thấp.");
            }
            else if (baoCao.NhietDo > 25)
            {
                SetFail(baoCao, "Nhiệt độ quá cao.");
            }
            else if (baoCao.DoAm < 40)
            {
                SetFail(baoCao, "Độ ẩm quá thấp.");
            }
            else if (baoCao.DoAm > 70)
            {
                SetFail(baoCao, "Độ ẩm quá cao.");
            }
            else
            {
                baoCao.TrangThai = 1;
                baoCao.GhiChuBC = null;
            }

            _context.SaveChanges();
        }

        private void SetFail(BaoCao baoCao, string ghiChu)
        {
            baoCao.TrangThai = 0;
            baoCao.GhiChuBC = ghiChu;
        }

        public List<BaoCao> Search(
            DateTime? fromDate,
            DateTime? toDate,
            int? maNV,
            int? maPhong)
        {
            var query = _context.BaoCaos
                .Include(b => b.NhanVien)
                .Include(b => b.Phong)
                .AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(b => b.NgayBC.Date >= fromDate.Value.Date);

            if (toDate.HasValue)
                query = query.Where(b => b.NgayBC.Date <= toDate.Value.Date);

            if (maNV.HasValue && maNV > 0)
                query = query.Where(b => b.MaNV == maNV.Value);

            if (maPhong.HasValue && maPhong > 0)
                query = query.Where(b => b.MaPhong == maPhong.Value);

            return query
                .OrderByDescending(b => b.NgayBC)
                .ToList();
        }

    }
}
