using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.DAL.Entities
{
    public class TaiKhoan
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = true;

        // FK → NhanVien
        public int MaNV { get; set; }
        public NhanVien NhanVien { get; set; } = null!;
    }
}
