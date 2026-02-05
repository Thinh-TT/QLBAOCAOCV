using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBAOCAOCV.DAL.Repositories;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepo;

        public AuthService(ITaiKhoanRepository taiKhoanRepo)
        {
            _taiKhoanRepo = taiKhoanRepo;
        }

        public TaiKhoan? Login(string username, string password)
        {
            var user = _taiKhoanRepo.GetByUsername(username);
            if (user == null) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValid) return null;

            return user;
        }
    }
}
