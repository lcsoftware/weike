using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface ISpecialtyBLL
    {
        #region 列表

        List<Specialty> Specialty_List(Specialty model, int PageIndex, int PageSize);

        #endregion

        #region 详细信息



        #endregion

        #region  对象修改或新增

        Specialty Specialty_Edit(Specialty model);

        #endregion

        #region  删除

        bool Specialty_Del(Specialty model);

        #endregion
    }
}
