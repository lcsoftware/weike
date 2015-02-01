using IES.G2S.JW.DAL;
using IES.AOP.G2S;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Cache; 


namespace IES.G2S.JW.BLL
{
    public class CfgSchoolBLL
    {

        /// <summary>
        /// 系统描述信息
        /// </summary>
        /// <returns></returns>
        public List<CfgSchool> CfgSchool_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "CfgSchool"))
            {
                List<CfgSchool> cfgschoollist = CfgSchoolDAL.CfgSchool_List();
                cache.Set( string.Empty, "CfgSchool", cfgschoollist );
                return cfgschoollist ;
            }
            else
            {
                return cache.Get<List<CfgSchool>>(string.Empty, "CfgSchool");
            }
        }
        /// <summary>
        /// 修改教师或学生的存储空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CfgSchoolSpace_Upd(CfgSchool model)
        {
            return CfgSchoolDAL.CfgSchoolSpace_Upd(model);
        }
    }
}
