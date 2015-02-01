using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.SYS.Model
{
    /// <summary>
    /// 用户授权信息
    /// </summary>
    public class UserAuInfo
    {
        /// <summary>
        /// 用户的基本信息
        /// </summary>
        //public User user { get; set; }

        /// <summary>
        /// 获取用户的模块列表
        /// </summary>
        public List<AuModule> modulelist { get; set; }

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        public List<AuRole>   rolelist { get; set; }


        /// <summary>
        /// 获取用户的模块行为列表
        /// </summary>
        public List<AuAction> actionlist { get; set; } 
        




    }
}
