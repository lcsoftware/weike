using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.G2S.JW.DAL;


namespace IES.G2S.JW.BLL
{
    public class ParmInfoBLL
    {
        #region 获取查询条件
        public ParmInfo Parm_Info_List()
        {
            return ParmInfoDAL.Parm_Info_List();
        }
        #endregion

        #region 根据一级学科获取二级学科
        public List<SpecialtyType> SpecialtyTyep2_List(SpecialtyType model)
        {
            return ParmInfoDAL.SpecialtyTyep2_List(model);
        }
        #endregion
    }
}
