using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.DAL.Entities;
using QLBAOCAOCV.DAL.Repositories;

namespace QLBAOCAOCV.BLL.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _repo;

        public TaiKhoanService(ITaiKhoanRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<TaiKhoan> GetAll()
        {
            return _repo.GetAll();
        }

        public void Create(TaiKhoan taiKhoan, string rawPassword)
        {
            taiKhoan.PasswordHash = BCrypt.Net.BCrypt.HashPassword(rawPassword);
            taiKhoan.IsActive = true;
            _repo.Add(taiKhoan);
        }

        public void ToggleActive(int userId)
        {
            var user = _repo.GetById(userId);
            if (user == null) return;

            user.IsActive = !user.IsActive;
            _repo.Update(user);
        }

        public void ResetPassword(int userId, string newPassword)
        {
            var user = _repo.GetById(userId);
            if (user == null) return;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _repo.Update(user);
        }
    }
}
