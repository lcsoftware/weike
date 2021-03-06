﻿/**  版本信息模板在安装目录下，可自行修改。
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
using System.Linq;
namespace IES.Resource.Model
{
    /// <summary>
    /// 课程章节表

    /// </summary>
    [Serializable]
    public partial class Chapter
    {
        #region 补充信息

        /// <summary>
        /// 快速设分
        /// </summary>
        public string FastScore { get; set; }
        public int FileNum { get; set; }


        public int TopicNum { get; set; }


        public int TestNum { get; set; }


        public int KenNum { get; set; }

        public int IsFinish { get; set; }

        public List<ChapterTest> ChapterTests { get; set; }

        public int IsTest { get; set; }
        //是否允许学习
        public int IsAllowStudy { get; set; }

        //是否被选中
        public bool IsActive { get; set; }

        /// <summary>
        /// 章节下习题数量
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

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 测试状态
        /// </summary>
        public int TestType { get; set; }

        /// <summary>
        /// 学习状态
        /// </summary>
        public int StudyType { get; set; }

        /// <summary>
        /// 章上绑定的测试ID
        /// </summary>
        public int TestID { get; set; }

        /// <summary>
        /// 学习进度
        /// </summary>
        public int StudyRate { get; set; }
        #endregion


        public Chapter()
        {
            Children = new List<Chapter>();
            ChapterTests = new List<ChapterTest>();
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

        /// <summary>
        /// 计划天数
        /// </summary>
        public int PlanDay { get; set; }

        public int MinHour { get; set; }

        public int MoocStatus { get; set; }
        /// <summary>
        /// 节测试id (,分割)
        /// </summary>
        public string TestIDs { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<Chapter> Children { get; set; }




        #endregion Model

    }
}

