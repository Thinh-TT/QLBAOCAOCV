using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.DAL.Entities
{
    public class BaoCao
    {
        [Key]
        public int MaBC { get; set; }

        public int MaNV { get; set; }
        public int MaPhong { get; set; }

        [Required(ErrorMessage = "Nhiệt độ không được để trống")]
        [Range(0, 50, ErrorMessage = "Nhiệt độ phải trong khoảng 0 - 50 °C")]
        public double NhietDo { get; set; }

        [Required(ErrorMessage = "Độ ẩm không được để trống")]
        [Range(0, 100, ErrorMessage = "Độ ẩm phải trong khoảng 0 - 100 %")]
        public double DoAm { get; set; }

        public DateTime NgayBC { get; set; } = DateTime.Now;

        public bool TrangThai { get; set; } = false;

        public DateTime? NgayXacNhan { get; set; }

        [MaxLength(255)]
        public string? GhiChuBC { get; set; }

        // Navigation
        public NhanVien? NhanVien { get; set; }
        public Phong? Phong { get; set; }
    }
}
