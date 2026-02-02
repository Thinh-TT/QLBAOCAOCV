using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.DAL.Entities
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenNV { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string EmailNV { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User"; // User | Manager

        // Navigation
        public ICollection<BaoCao>? BaoCaos { get; set; }
    }
}
