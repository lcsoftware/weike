/**  版本信息模板在安装目录下，可自行修改。
* Schoolzone.cs
*
* 功 能： N/A
* 类 名： Schoolzone
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:34   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.JW.Model
{
	/// <summary>
	/// 课表
	/// </summary>
	[Serializable]
	public partial class Schoolzone
	{
		public Schoolzone()
		{}
		#region Model
		private int _zoneid;
		private string _zoneno;
		private string _zonename;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int ZoneID
		{
			set{ _zoneid=value;}
			get{return _zoneid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZoneNo
		{
			set{ _zoneno=value;}
			get{return _zoneno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZoneName
		{
			set{ _zonename=value;}
			get{return _zonename;}
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

