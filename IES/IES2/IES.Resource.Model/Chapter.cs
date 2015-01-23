/**  版本信息模板在安装目录下，可自行修改。
* Chapter.cs
*
* 功 能： N/A
* 类 名： Chapter
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
[assembly: CLSCompliant(true)]
namespace IES.Resource.Model
{
    /// <summary>
    /// 课程章节表
    /// </summary>
    [Serializable]
    public partial class Chapter
    {
        #region 补充信息

        public int FileNum { get; set; }


        public int TopicNum { get; set; }


        public int TestNum { get; set; }


        public int KenNum { get; set; }


        #endregion 


        public Chapter()
        {
            //this.Children = new List<Chapter>();
        }
        #region Model
        private int _chapterid;
        private int _ocid = 0;
        private int _courseid = 0;
        private int _owneruserid = 0;
        private int _createuserid = 0;
        private string _title;
        private int _parentid = 0;
        private int? _orde = 1;

        //public IList<Chapter> Children { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int ChapterID
        {
            set { _chapterid = value; }
            get { return _chapterid; }
        }
        /// <summary>
        /// 在线课程编号
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 章节对应的课程编号
        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 资源拥有人编号
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
        /// 章节标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 上级章节编号
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Orde
        {
            set { _orde = value; }
            get { return _orde; }
        }


        public string Brief { get; set; }

        public int PlanDay { get; set; }

        public int MinHour { get; set; }


        #endregion Model

    }
}

