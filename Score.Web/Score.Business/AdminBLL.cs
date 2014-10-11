/* **************************************************************
* Copyright(c) 2014 Score.web, All Rights Reserved.   
* File             : SystemBLL.cs
* Description      : 系统数据访问
* Author           : Fenglujian 
* Created          : 2014-10-01  
* Revision History : 
******************************************************************/
namespace App.Score.Business
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using App.Score.Entity;

    /// <summary>
    /// 系统数据访问 
    /// </summary>
    public class AdminBLL : App.Score.Data.AppBLL
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="user">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns>返回登录用户信息</returns>
        public UserEntry Verify(string user, string pwd)
        {
            UserEntry userEntry = this.GetDataItem<UserEntry>("USP_System_Verify", new { User = user, Pwd = pwd });
            return userEntry;
        }

        /// <summary>
        /// 返回用户权限
        /// </summary>
        /// <param name="teacherID"></param>
        /// <returns></returns>
        public List<FuncEntry> GetFuncs(string teacherID)
        {
            return this.FillList<FuncEntry>("s_p_getTeacherRight", new { TeacherID = teacherID });
        }
    }
}

