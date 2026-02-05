using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.DAL.Entities
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenPhong { get; set; } = string.Empty;

        // Navigation
        public ICollection<BaoCao>? BaoCaos { get; set; } = new List<BaoCao>();
    }
}
