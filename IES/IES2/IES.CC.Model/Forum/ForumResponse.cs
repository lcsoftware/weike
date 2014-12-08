/**  版本信息模板在安装目录下，可自行修改。
* ForumResponse.cs
*
* 功 能： N/A
* 类 名： ForumResponse
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
	/// 答疑讨论回复
	/// </summary>
	[Serializable]
	public partial class ForumResponse
	{
		public ForumResponse()
		{}
		#region Model
		private int _responseid;
		private int _topicid;
		private int _parentid=0;
		private string _conten;
		private int _userid;
		private DateTime _updatetime= DateTime.Now;
		private bool _istop= false;
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
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 回复人编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
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
		#endregion Model

	}
}

