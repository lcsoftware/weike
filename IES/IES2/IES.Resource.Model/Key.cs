/**  版本信息模板在安装目录下，可自行修改。
* Key.cs
*
* 功 能： N/A
* 类 名： Key
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:50   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.Resource.Model
{
    /// <summary>
    /// 标签表、关键字表
    /// </summary>
    [Serializable]
    public partial class Key
    {
        #region  补充属性
        /// <summary>
        /// 标签下习题数
        /// </summary>
        public int ExerciseCount { get; set; }
        #endregion
        public Key()
        { }
        #region Model
        private int _keyid;
        private string _name;
        private int _owneruserid = 0;
        private int? _createuserid;
        private int _courseid = 0;
        private int? _ocid = 0;
        /// <summary>
        /// 
        /// </summary>
        public int KeyID
        {
            set { _keyid = value; }
            get { return _keyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OwnerUserID
        {
            set { _owneruserid = value; }
            get { return _owneruserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        #endregion Model

    }
}

