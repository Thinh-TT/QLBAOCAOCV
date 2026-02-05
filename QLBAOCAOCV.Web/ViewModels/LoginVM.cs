using System.ComponentModel.DataAnnotations;

namespace QLBAOCAOCV.Web.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Nhap username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Nhap password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
