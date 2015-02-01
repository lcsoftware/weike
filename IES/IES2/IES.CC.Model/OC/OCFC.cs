/**  版本信息模板在安装目录下，可自行修改。
* OCFC.cs
*
* 功 能： N/A
* 类 名： OCFC
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:22   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.OC.Model
{
    /// <summary>
    /// 翻转课堂主表
    /// </summary>
    [Serializable]
    public partial class OCFC
    {
        #region 补充属性
        /// <summary>
        /// 翻转课堂学生总数
        /// </summary>
        public int FCStudentCount { get; set; }
        /// <summary>
        /// 翻转课堂小组总数
        /// </summary>
        public int FCLiveGroupCount { get; set; }
        /// <summary>
        /// 教学班名字
        /// </summary>
        public string FCTeachingClassNames { get; set; }
        /// <summary>
        /// 资料总数
        /// </summary>
        public string FCFileCount { get; set; }
        /// <summary>
        /// 必读资料数
        /// </summary>
        public string FCFileMustCount { get; set; }
        /// <summary>
        /// 作业总数
        /// </summary>
        public string FCExamCount { get; set; }
        /// <summary>
        /// 互动总数
        /// </summary>
        public string FCLiveCount { get; set; }
        #endregion

        public OCFC()
        { }
        #region Model
        private int _fcid;
        private int _ocid;
        private int _userid;
        private DateTime _startweek;
        private DateTime _endweek;
        private DateTime _updatetime;
        private string _brief;
        private int _orde = 1;
        private bool _isdeleted = false;
        private DateTime _endtime;
        /// <summary>
        /// 翻转课堂id
        /// </summary>
        public int FCID
        {
            set { _fcid = value; }
            get { return _fcid; }
        }
        /// <summary>
        /// 翻转课堂课程id
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 创建人id
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startweek = value; }
            get { return _startweek; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _endweek = value; }
            get { return _endweek; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
        /// <summary>
        /// 学习说明
        /// </summary>
        public int Orde
        {
            set { _orde = value; }
            get { return _orde; }
        }


        /// <summary>
        /// 分组模式
        /// </summary>
        public int GroupModeID { get; set; }
        /// <summary>
        /// 视频学习间隔
        /// </summary>
        public int PopTime { get; set; }

        /// <summary>
        /// 0
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }






        #endregion Model

    }
}

