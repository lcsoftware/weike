/* **************************************************************
  * Copyright(c) 2014 Score.web, All Rights Reserved.   
  * File             : User.cs
  * Description      : 用户相关实体类定义
  * Author           : Fenglujian 
  * Created          : 2014-10-01  
  * Revision History : 
******************************************************************/
[assembly: System.CLSCompliant(true)] 
namespace App.Score.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    /// <summary>
    /// 功能
    /// </summary>
    public class FuncEntry
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string FuncID;
        /// <summary>
        /// 名称
        /// </summary>
        public string FuncName;
        /// <summary>
        /// 类型
        /// </summary>
        public string FuncType;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 父功能
        /// </summary>
        public string FuncID0;
    }

    public class UserInfo
    {
        public string TeacherID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string UserOrGroup { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string IsMarry { get; set; }
        public string NationNo { get; set; }
        public string PoliticCode { get; set; }
        public string ResidentNo { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; } 
        public string GroupID { get; set; }
    }

    public class GroupUser
    { 
        public string GroupID;
        public string Name;
        public string Description;
        public string UserOrGroup;
        public IList<UserInfo> Children = new List<UserInfo>();
    }
}
