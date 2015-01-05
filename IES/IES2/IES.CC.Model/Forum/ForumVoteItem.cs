/**  版本信息模板在安装目录下，可自行修改。
* ForumVoteItem.cs
*
* 功 能： N/A
* 类 名： ForumVoteItem
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
	/// 论坛投票选项
	/// </summary>
	[Serializable]
	public partial class ForumVoteItem
	{
		public ForumVoteItem()
		{}
		#region Model
		private int _voteitemid;
		private int _voteid;
		private string _conten;
		private int _orde=1;
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
		public int VoteID
		{
			set{ _voteid=value;}
			get{return _voteid;}
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
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

