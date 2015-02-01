/**  版本信息模板在安装目录下，可自行修改。
* Sys.cs
*
* 功 能： N/A
* 类 名： Sys
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:46   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.JW.Model
{
    /// <summary>
    /// IES系统
    /// </summary>
    [Serializable]
    public partial class Sys
    {
        public Sys()
        { }
        #region 补充信息
        public int RoleID { get; set; }
        #endregion
        #region Model
        private int _sysid;
        private string _name;
        private string _enname;
        /// <summary>
        /// 
        /// </summary>
        public int sysid
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string enname
        {
            set { _enname = value; }
            get { return _enname; }
        }
        #endregion Model

    }
}

