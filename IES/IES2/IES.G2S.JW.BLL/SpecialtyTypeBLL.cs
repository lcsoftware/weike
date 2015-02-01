using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.JW.DAL;
using IES.JW.Model;

namespace IES.G2S.JW.BLL
{
    public class SpecialtyTypeBLL
    {

        #region 列表
        public List<SpecialtyType> SpecialtyType_Tree_List()
        {
            return SpecialtyTypeDAL.SpecialtyType_Tree_List();
        }
        #endregion

        #region 编辑
        public SpecialtyType SpecialtyType_Edit(SpecialtyType model)
        {
            return SpecialtyTypeDAL.SpecialtyType_Edit(model);
        }
        #endregion

        #region 删除
        public bool SpecialtyType_Del(SpecialtyType model)
        {
            return SpecialtyTypeDAL.SpecialtyType_Del(model);
        }
        #endregion

        #region 取消删除
        public bool SpecialtyType_CancelDel(SpecialtyType model)
        {
            return SpecialtyTypeDAL.SpecialtyType_CancelDel(model);
        }
        #endregion

        #region 上级学科
        public List<SpecialtyType> SpecialtyType_P_List()
        {
            return SpecialtyTypeDAL.SpecialtyType_P_List();
        }
        #endregion
    }
}
