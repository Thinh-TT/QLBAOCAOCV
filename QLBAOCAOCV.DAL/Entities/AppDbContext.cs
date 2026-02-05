using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.DAL.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<NhanVien> NhanViens => Set<NhanVien>();
        public DbSet<Phong> Phongs => Set<Phong>();
        public DbSet<BaoCao> BaoCaos => Set<BaoCao>();
        public DbSet<TaiKhoan> TaiKhoans { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaiKhoan>()
                .HasOne(t => t.NhanVien)
                .WithOne(nv => nv.TaiKhoan)
                .HasForeignKey<TaiKhoan>(t => t.MaNV);

            modelBuilder.Entity<BaoCao>()
                .HasOne(b => b.NhanVien)
                .WithMany(n => n.BaoCaos)
                .HasForeignKey(b => b.MaNV);

            modelBuilder.Entity<BaoCao>()
                .HasOne(b => b.Phong)
                .WithMany(p => p.BaoCaos)
                .HasForeignKey(b => b.MaPhong);
        }

        
    }
}
