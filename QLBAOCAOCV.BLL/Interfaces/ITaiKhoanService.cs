using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.BLL.Interfaces
{
    public interface ITaiKhoanService
    {
        IEnumerable<TaiKhoan> GetAll();
        void Create(TaiKhoan taiKhoan, string rawPassword);
        void ToggleActive(int userId);
        void ResetPassword(int userId, string newPassword);
    }
}
