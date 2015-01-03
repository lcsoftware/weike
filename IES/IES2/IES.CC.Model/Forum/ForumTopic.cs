/**  版本信息模板在安装目录下，可自行修改。
* ForumTopic.cs
*
* 功 能： N/A
* 类 名： ForumTopic
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:03   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Forum.Model
{
	/// <summary>
	/// 论坛主题
	/// </summary>
	[Serializable]
	public partial class ForumTopic
	{
		public ForumTopic()
		{}
		#region Model
		private int _topicid;
		private int _courseid;
        private int _forumtypeid;
		private int _userid;
		private string _title;
		private string _conten;
		private DateTime _updatetime;
		private int _clicks=0;
		private bool _istop= false;
		private bool _isessence= false;
		private bool _isclosed= false;
		private int _topictype=0;
		private string _tags;
		/// <summary>
		/// 
		/// </summary>
		public int TopicID
		{
			set{ _topicid=value;}
			get{return _topicid;}
		}

        public int OCID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}

        /// <summary>
        /// 论坛版块论 坛版块编号,0为小组讨论或MOOC章节讨论
        /// </summary>
        public int ForumTypeID
        {
            set { _forumtypeid = value; }
            get { return _forumtypeid; }
        }


        /// <summary>
        /// 小组讨论的任务编号
        /// </summary>
        public int GroupTaskID { set; get; }

        /// <summary>
        /// 主题对应的章节编号
        /// </summary>
        public int ChapterID { set; get; }

		/// <summary>
		/// 发帖人
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

        /// <summary>
        /// 发帖人姓名
        /// </summary>
        public string  UserName { set;get;   }



		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}

		/// <summary>
		/// 置顶
		/// </summary>
		public bool IsTop
		{
			set{ _istop=value;}
			get{return _istop;}
		}
		/// <summary>
		/// 是否精华主题
		/// </summary>
		public bool IsEssence
		{
			set{ _isessence=value;}
			get{return _isessence;}
		}
		/// <summary>
		/// 主题是否关闭
		/// </summary>
		public bool IsClosed
		{
			set{ _isclosed=value;}
			get{return _isclosed;}
		}
		/// <summary>
        /// 0常规主题，  1 FC主题 ， 2 MOOC主题；3小组讨论主题
		/// </summary>
		public int TopicType
		{
			set{ _topictype=value;}
			get{return _topictype;}
		}
		/// <summary>
		/// 标签，多个之间用空格
		/// </summary>
		public string Tags
		{
			set{ _tags=value;}
			get{return _tags;}
		}

        /// <summary>
        /// 最后回帖人姓名
        /// </summary>
        public string LastUserName { set; get; }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        public DateTime LastUpdateTime { set; get; }


        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Clicks
        {
            set { _clicks = value; }
            get { return _clicks; }
        }

        /// <summary>
        /// 回复总数
        /// </summary>
        public int Responses { get; set; }

        /// <summary>
        /// 被赞的总数
        /// </summary>
        public int Goods { get; set; }


		#endregion Model

	}
}

