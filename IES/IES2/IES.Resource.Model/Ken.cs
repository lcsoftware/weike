/**  版本信息模板在安装目录下，可自行修改。
* Ken.cs
*
* 功 能： N/A
* 类 名： Ken
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:49   N/A    初版
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
    /// 知识点
    /// </summary>
    [Serializable]
    public partial class Ken
    {
        #region
        /// <summary>
        /// 知识点下的习题数
        /// </summary>
        public int ExerciseCount { get; set; }
        /// <summary>
        /// 抽题数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 题目分数
        /// </summary>
        public int Scoreper { get; set; }
        #endregion

        public Ken()
        { }
        #region Model
        private int _kenid;
        private int _courseid = 0;
        private int _ocid = 0;
        private int _owneruserid = 0;
        private int _createuserid = 0;
        private string _name;
        private string _pingyin;
        private int _requirement = 1;
        private DateTime _updatetime = DateTime.Now;
        /// <summary>
        /// 主键
        /// </summary>
        public int KenID
        {
            set { _kenid = value; }
            get { return _kenid; }
        }
        /// <summary>
        /// 课程编号
        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }

        /// <summary>
        /// 知识点对应的章节编号
        /// </summary>
        public int ChapterID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OwnerUserID
        {
            set { _owneruserid = value; }
            get { return _owneruserid; }
        }
        /// <summary>
        /// 创建人编号
        /// </summary>
        public int CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 知识点名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 拼音
        /// </summary>
        public string Pingyin
        {
            set { _pingyin = value; }
            get { return _pingyin; }
        }
        /// <summary>
        /// 了解,理解,掌握
        /// </summary>
        public int Requirement
        {
            set { _requirement = value; }
            get { return _requirement; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}

