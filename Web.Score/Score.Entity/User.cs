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
    /// 用户实体类
    /// </summary>
    public class UserEntry
    {
        /// <summary>
        /// string 类型编号 
        /// </summary>
        public string TeacherID;

        /// <summary>
        /// 登录名
        /// </summary>
        public string Name;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description;

        public override string ToString()
        {
            return string.Format("id:{0}, name:{1}", this.TeacherID, this.Name);
        }
    }

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
}
