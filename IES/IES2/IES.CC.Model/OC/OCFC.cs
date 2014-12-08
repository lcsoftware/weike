/**  版本信息模板在安装目录下，可自行修改。
* OCFC.cs
*
* 功 能： N/A
* 类 名： OCFC
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:22   N/A    初版
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
	/// 翻转课堂主表
	/// </summary>
	[Serializable]
	public partial class OCFC
	{
		public OCFC()
		{}
		#region Model
		private int _fcid;
		private int _ocid;
		private int _userid;
		private int _startweek;
		private int _endweek;
		private DateTime _updatetime;
		private string _brief;
		private int _orde=1;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int FCID
		{
			set{ _fcid=value;}
			get{return _fcid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
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
		public int StartWeek
		{
			set{ _startweek=value;}
			get{return _startweek;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EndWeek
		{
			set{ _endweek=value;}
			get{return _endweek;}
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
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 学习说明
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		/// <summary>
		/// 0
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

