/**  版本信息模板在安装目录下，可自行修改。
* ForumMy.cs
*
* 功 能： N/A
* 类 名： ForumMy
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:02   N/A    初版
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
	/// 我与主题的关注，点赞等
	/// </summary>
	[Serializable]
	public partial class ForumMy
	{
		public ForumMy()
		{}
		#region Model
		private int _forummyid;
		private int _topicid;
		private int _responseid=0;
		private bool _isgood= false;
		private bool _isattention;
		private DateTime _readdate= DateTime.Now;
		private int _score=0;
		/// <summary>
		/// 
		/// </summary>
		public int ForumMyID
		{
			set{ _forummyid=value;}
			get{return _forummyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TopicID
		{
			set{ _topicid=value;}
			get{return _topicid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ResponseID
		{
			set{ _responseid=value;}
			get{return _responseid;}
		}
		/// <summary>
		/// 我是否点了赞
		/// </summary>
		public bool IsGood
		{
			set{ _isgood=value;}
			get{return _isgood;}
		}
		/// <summary>
		/// 我是否关注了该贴
		/// </summary>
		public bool IsAttention
		{
			set{ _isattention=value;}
			get{return _isattention;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ReadDate
		{
			set{ _readdate=value;}
			get{return _readdate;}
		}
		/// <summary>
		/// 主题得分
		/// </summary>
		public int Score
		{
			set{ _score=value;}
			get{return _score;}
		}

        /// <summary>
        /// 关注用户编号
        /// </summary>
        public int UserID
        {
            get;
            set;
        }

		#endregion Model

	}
}

