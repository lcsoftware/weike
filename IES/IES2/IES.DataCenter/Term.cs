/**  版本信息模板在安装目录下，可自行修改。
* Term.cs
*
* 功 能： N/A
* 类 名： Term
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:05   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.DataCenter.Model
{
	/// <summary>
	/// 校历
	/// </summary>
	[Serializable]
	public partial class Term
	{
		public Term()
		{}
		#region Model
		private int _termid;
		private string _termno;
		private string _termyear;
		private int _termtypeid;
		private DateTime _startdate;
		private DateTime _enddate;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int TermID
		{
			set{ _termid=value;}
			get{return _termid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TermNo
		{
			set{ _termno=value;}
			get{return _termno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TermYear
		{
			set{ _termyear=value;}
			get{return _termyear;}
		}
		/// <summary>
		/// 学期 1,2,3,4
		/// </summary>
		public int TermTypeID
		{
			set{ _termtypeid=value;}
			get{return _termtypeid;}
		}
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

