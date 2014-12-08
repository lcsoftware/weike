using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.SYS.Model
{
    /// <summary>
    /// 用户权限、身份信息类
    /// </summary>
    public class AuUserInfo
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 用户角色列表
        /// </summary>
        public List<AuUserRoleOrg> auuserroleorglist { get; set; }

        /// <summary>
        /// 角色列表模块及行为列表
        /// </summary>
        public List<AuRoleModule> aurolemodulelist { get; set; }
    }
}
