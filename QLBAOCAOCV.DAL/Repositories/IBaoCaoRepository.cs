using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLBAOCAOCV.DAL.Entities;

namespace QLBAOCAOCV.DAL.Repositories
{
    public interface IBaoCaoRepository
    {
        IQueryable<BaoCao> GetAll();

        BaoCao? GetById(int id);

        void Update(BaoCao baoCao);
    }
}
