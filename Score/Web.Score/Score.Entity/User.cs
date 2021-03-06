﻿/* **************************************************************
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

    public enum FuncKind
    {
        Group = 1,              //用户权限来源于所属组
        SuperUser = 2,          //超级用户赋予的权限
        None = 0,               //无此权限
    }

    /// <summary>
    /// 功能
    /// </summary>
    public class FuncEntry
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int FuncID;
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
        public int Parent;
        /// <summary>
        /// 系统编号
        /// </summary>
        public int SysNo;

        /// <summary>
        /// 控制功能显隐
        /// </summary>
        public bool Visibled = false;
        /// <summary>
        /// 分类 用于权限编辑
        /// </summary>
        public FuncKind Kind = FuncKind.None;

        /// <summary>
        /// 子功能
        /// </summary>
        public IList<FuncEntry> Children = new List<FuncEntry>();
    }

    public class UserGroupInfo
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

        public bool Selected { get; set; }

        public IList<UserGroupInfo> Children = new List<UserGroupInfo>();
    }

    public class UserGroupFunc
    {
        public string TeacherID { get; set; }
        public string GroupID { get; set; }
        public int FuncID { get; set; }

        public int UserOrGroup {get; set;}
    }

    public class UserAuth
    {
        public string FuncId;
        public string FuncName;
        public string Description;
        public int FuncType;
        public string FuncTypeName
        {
            get
            {
                switch (FuncType)
                {
                    case 0: return "菜单";
                    case 2: return "系统";
                    default: return "保留";
                }
            }
        }
    }

    //public class GroupUser
    //{ 
    //    public string GroupID;
    //    public string Name;
    //    public string Description;
    //    public string UserOrGroup;
    //    public IList<UserInfo> Children = new List<UserInfo>();
    //}
}
