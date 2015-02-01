/**  版本信息模板在安装目录下，可自行修改。
* Notice.cs
*
* 功 能： N/A
* 类 名： Notice
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
	/// 系统通知
	/// </summary>
	[Serializable]
	public partial class Notice
    {      

        public Notice()
		{}

        #region  补充信息
        public string Key { get; set; }

        public string Source { get; set; }
        public string SourceIDs { get; set; }
        public string Source2 { get; set; }
        public string SourceIDs2 { get; set; }
        public string EntryDates { get; set; }
        public int IsCanSendMsg { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 列表总数
        /// </summary>
        public int rowscount { get; set; }

        #endregion 

        #region Model
        private int _NoticeID;
        private string _Title;
        private string _Conten;
        private DateTime _UpdateTime;
        private bool _IsTop;
        private bool _IsForMail;
        private bool _IsForSMS;
        private DateTime _EndDate;
        private int _SysID;
        private int _UserID;
        private int _Responses;
     

        /// <summary>
        /// 通知编号
        /// </summary>
        public int NoticeID
        {
            set { _NoticeID = value; }
            get { return _NoticeID; }
        }

        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string Conten
        {
            set { _Conten = value; }
            get { return _Conten; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            set { _IsTop = value; }
            get { return _IsTop; }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsForMail
        {
            set { _IsForMail = value; }
            get { return _IsForMail; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsForSMS
        {
            set { _IsForSMS = value; }
            get { return _IsForSMS; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndDate
        {
            set { _EndDate = value; }
            get { return _EndDate; }
        }

        /// <summary>
        /// 子系统编号
        /// </summary>
        public int SysID
        {
            set { _SysID = value; }
            get { return _SysID; }
        }

        /// <summary>
        /// 通知对应的模块编号
        /// </summary>
        public int ModuleID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }
        public int Responses
        {
            set { _Responses = value; }
            get { return _Responses; }
        }
        #endregion

	}
}

