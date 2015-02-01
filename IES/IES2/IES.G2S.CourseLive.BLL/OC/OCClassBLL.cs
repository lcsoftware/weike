using IES.CC.OC.Model;
using IES.G2S.CourseLive.DAL.OC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.CourseLive.BLL.OC
{
    /// <summary>
    /// 网络教学班
    /// </summary>
    public class OCClassBLL
    {
        #region 列表
        /// <summary>
        /// 网络教学班下拉列表
        /// 2015年1月10日11:55:42
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<OCClass> OCClass_Dropdown_List(int OCID)
        {
            return OCClassDAL.OCClass_Dropdown_List(OCID);
        } 
        #endregion
    }
}
