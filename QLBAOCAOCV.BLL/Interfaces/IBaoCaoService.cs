using QLBAOCAOCV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBAOCAOCV.BLL.Interfaces
{
    public interface IBaoCaoService
    {
        List<BaoCao> GetAll();

        BaoCao? GetById(int maBC);

        void Create(BaoCao baoCao);

        void XacNhanBaoCao(int maBC);

        List<BaoCao> Search(
        DateTime? fromDate,
        DateTime? toDate,
        int? maNV,
        int? maPhong
        );

    }
}
