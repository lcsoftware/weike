/**  版本信息模板在安装目录下，可自行修改。
* OCNoticeResponse.cs
*
* 功 能： N/A
* 类 名： OCNoticeResponse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:29   N/A    初版
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
	/// 通知回复
	/// </summary>
	[Serializable]
	public partial class OCNoticeResponse
	{
		public OCNoticeResponse()
		{}
		#region Model
		private int _responseid;
		private int _noticeid;
		private int _userid;
		private DateTime _updatetime= DateTime.Now;
		private string _conten;
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
		public int NoticeID
		{
			set{ _noticeid=value;}
			get{return _noticeid;}
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
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		#endregion Model

	}
}

