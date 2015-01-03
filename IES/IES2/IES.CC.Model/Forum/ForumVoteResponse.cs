/**  版本信息模板在安装目录下，可自行修改。
* ForumVoteResponse.cs
*
* 功 能： N/A
* 类 名： ForumVoteResponse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 13:12:05   N/A    初版
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
	/// 论坛参与人投票
	/// </summary>
	[Serializable]
	public partial class ForumVoteResponse
	{
		public ForumVoteResponse()
		{}
		#region Model
		private int _responseid;
		private int _topicid;
		private int _voteid;
		private int _voteitemid;
		private int _userid;
		/// <summary>
		/// 
		/// </summary>
		public int ResponseID
		{
			set{ _responseid=value;}
			get{return _responseid;}
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
		public int VoteID
		{
			set{ _voteid=value;}
			get{return _voteid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int VoteItemID
		{
			set{ _voteitemid=value;}
			get{return _voteitemid;}
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
        /// 该元素投票统计,改属性属于proc数据
        /// </summary>
        public int Proc_VoteCount { get; set; }
        



		#endregion Model

	}
}

