using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.DAL.Repositories
{
    public interface ITaiKhoanRepository
    {
        TaiKhoan? GetByUsername(string username);

        TaiKhoan? GetById(int userId);
        void Add(TaiKhoan taiKhoan);
        void Update(TaiKhoan taiKhoan);

        IEnumerable<TaiKhoan> GetAll();
    }
}
