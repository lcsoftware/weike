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

        public List<Specialty> Specialty_List(Specialty model, int PageIndex, int PageSize)
        {
            return SpecialtyDAL.Specialty_List(model, PageIndex, PageSize);
        }
        public List<ShortSpecialty> Specialty_Short_List(ShortSpecialty model)
        {
            return SpecialtyDAL.Specialty_Short_List(model);
        }
        #endregion

        #region 详细信息

        public SpecialtyInfo SpecialtyInfo_Get(SpecialtyInfo model)
        {
            return SpecialtyDAL.SpecialtyInfo_Get(model);
        }

        #endregion

        #region  对象修改或新增

        public Specialty Specialty_Edit(Specialty model)
        {
            return SpecialtyDAL.Specialty_Edit(model);
        }

        #endregion

        #region 删除

        public bool Specialty_Del(Specialty model)
        {
            return SpecialtyDAL.Specialty_Del(model);
        }

        #endregion

        #region  获取全部信息

        public List<Specialty> NewsSpecialty_List()
        {
            return SpecialtyDAL.NewsSpecialty_List();
        }

        #endregion

        #region 批量删除
        public bool Specialty_Batch_Del(string IDS)
        {
            return SpecialtyDAL.Specialty_Batch_Del(IDS);
        }

        #endregion

        #region 学科树
        public List<SpecialtyType> SpecialtyType_Tree_List()
        {
            return SpecialtyDAL.SpecialtyType_Tree_List();
        }
        #endregion

        #region 上级学科
        public List<SpecialtyType> SpecialtyType_P_List()
        {
            return SpecialtyDAL.SpecialtyType_P_List();
        }
        #endregion
    }
}
