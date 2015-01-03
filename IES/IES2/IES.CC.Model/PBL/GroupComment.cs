/**  版本信息模板在安装目录下，可自行修改。
* GroupComment.cs
*
* 功 能： N/A
* 类 名： GroupComment
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 小组事务
	/// </summary>
	[Serializable]
	public partial class GroupComment
	{
		public GroupComment()
		{}
		#region Model
		private int _commentid;
		private int? _resultid;
		private string _conten;
		private int? _userid;
		private DateTime? _createtime= DateTime.Now;
		/// <summary>
		/// 评论编号
		/// </summary>
		public int CommentID
		{
			set{ _commentid=value;}
			get{return _commentid;}
		}
		/// <summary>
		/// 成果编号
		/// </summary>
		public int? ResultID
		{
			set{ _resultid=value;}
			get{return _resultid;}
		}
		/// <summary>
		/// 评论内容
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 评论人用户编号
		/// </summary>
		public int? UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 评论时间
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

