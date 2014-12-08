/**  版本信息模板在安装目录下，可自行修改。
* ForumVote.cs
*
* 功 能： N/A
* 类 名： ForumVote
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:04   N/A    初版
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
	/// 发起论坛投票
	/// </summary>
	[Serializable]
	public partial class ForumVote
	{
		public ForumVote()
		{}
		#region Model
		private int _voteid;
		private int _topicid;
		private int _userid;
		private DateTime _expireddate;
		private bool _ismust= false;
		private bool _isvoteshowresult= true;
		private bool _type= false;
		private int _votemin=0;
		private int _votemax=0;
		private DateTime _updatetime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int VoteID
		{
			set{ _voteid=value;}
			get{return _voteid;}
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
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 过期时间
		/// </summary>
		public DateTime ExpiredDate
		{
			set{ _expireddate=value;}
			get{return _expireddate;}
		}
		/// <summary>
		/// 是否强制投票
		/// </summary>
		public bool IsMust
		{
			set{ _ismust=value;}
			get{return _ismust;}
		}
		/// <summary>
		/// 学生投票后才能查看投票结果
		/// </summary>
		public bool IsVoteShowResult
		{
			set{ _isvoteshowresult=value;}
			get{return _isvoteshowresult;}
		}
		/// <summary>
		/// 0 单选  ； 1 多选
		/// </summary>
		public bool Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 允许学生选择的最少数量 
		/// </summary>
		public int VoteMin
		{
			set{ _votemin=value;}
			get{return _votemin;}
		}
		/// <summary>
		/// 允许学生选择的最大数量 ,如果是1表示是单选.
		/// </summary>
		public int VoteMax
		{
			set{ _votemax=value;}
			get{return _votemax;}
		}
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

