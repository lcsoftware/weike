using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class SpecialtyBLL:ISpecialtyBLL
    {
        #region  列表

        public  List<Specialty> Specialty_List(Specialty model,int PageSize,int PageIndex)
        {
            return SpecialtyDAL.Specialty_List(model,PageSize,PageIndex);
        }

        #endregion

        #region  新增

        public Specialty Specialty_ADD(Specialty model)
        {
            return SpecialtyDAL.Specialty_ADD(model);
        }
        #endregion

        #region  对象更新

        public bool Specialty_Upd(Specialty model)
        {
            return SpecialtyDAL.Specialty_Upd(model);
        }

        #endregion

        #region 删除

        public bool Specialty_Del(Specialty model)
        {
            return SpecialtyDAL.Specialty_Del(model);
        }

        #endregion
    }
}
