using System.ComponentModel.DataAnnotations;

namespace QLBAOCAOCV.Web.ViewModels
{
    public class BaoCaoCreateVM
    {
        [Required]
        public int MaPhong { get; set; }

        [Required]
        public float NhietDo { get; set; }

        [Required]
        public float DoAm { get; set; }

        public string? GhiChuBC { get; set; }
    }
}
