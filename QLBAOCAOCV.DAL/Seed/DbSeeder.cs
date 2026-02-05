using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using QLBAOCAOCV.DAL.Entities;


namespace QLBAOCAOCV.DAL.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.TaiKhoans.Any())
                return; // Da co data

            // ===== NHAN VIEN =====
            var nvAdmin = new NhanVien
            {
                TenNV = "Admin Demo",
                EmailNV = "admin@demo.com"
            };

            var nvUser = new NhanVien
            {
                TenNV = "User Demo",
                EmailNV = "user@demo.com"
            };

            context.NhanViens.AddRange(nvAdmin, nvUser);
            context.SaveChanges();

            // ===== TAI KHOAN =====
            var adminAccount = new TaiKhoan
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Admin",
                MaNV = nvAdmin.MaNV,
                IsActive = true
            };

            var userAccount = new TaiKhoan
            {
                Username = "user",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "User",
                MaNV = nvUser.MaNV,
                IsActive = true
            };

            context.TaiKhoans.AddRange(adminAccount, userAccount);
            context.SaveChanges();
        }
    }
}
