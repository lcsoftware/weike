using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.SYS.Model;
using IES.CC.OC.Model;
using IES.G2S.SYS.DAL ;
using IES.Cache;

namespace IES.Common.Data
{

    /// <summary>
    /// 获取用户相关的数据，比如用户的权限、用户相关的课程
    /// </summary>
    public class UserCommonData 
    {

        /// <summary>
        /// 获取用户的在线课程列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<OC> User_OC_Get(User model )
        {
            return  UserDAL.User_OC_Get(model);
        }

        /// <summary>
        /// 获取用户的授权模块列表，模块的行为列表
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public static UserAuInfo UserAuInfo_Info_Get(int userid)
        {
            return new UserAuInfo();
        }






    }
}
