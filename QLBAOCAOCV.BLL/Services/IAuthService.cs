using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.BLL.Services
{
    public interface IAuthService
    {
        TaiKhoan? Login(string username, string password);
    }
}
