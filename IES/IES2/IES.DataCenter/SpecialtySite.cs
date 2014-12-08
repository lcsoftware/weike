/**  版本信息模板在安装目录下，可自行修改。
* SpecialtySite.cs
*
* 功 能： N/A
* 类 名： SpecialtySite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:01   N/A    初版
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
	/// 专业课程网站
	/// </summary>
	[Serializable]
	public partial class SpecialtySite
	{
		public SpecialtySite()
		{}
		#region Model
		private int _specialtysiteid;
		private int _specialtyid;
		private int _ocid;
		/// <summary>
		/// 
		/// </summary>
		public int SpecialtySiteID
		{
			set{ _specialtysiteid=value;}
			get{return _specialtysiteid;}
		}
		/// <summary>
		/// 专业编号
		/// </summary>
		public int SpecialtyID
		{
			set{ _specialtyid=value;}
			get{return _specialtyid;}
		}
		/// <summary>
		/// 在线课程编号
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		#endregion Model

	}
}

