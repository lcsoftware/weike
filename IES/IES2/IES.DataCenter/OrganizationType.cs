/**  版本信息模板在安装目录下，可自行修改。
* OrganizationType.cs
*
* 功 能： N/A
* 类 名： OrganizationType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:34:59   N/A    初版
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
	/// 教学组织类型
	/// </summary>
	[Serializable]
	public partial class OrganizationType
	{
		public OrganizationType()
		{}
		#region Model
		private int _organizationtypeid;
		private string _organizationtypeno;
		private string _organizationtypename;
		/// <summary>
		/// 
		/// </summary>
		public int OrganizationTypeID
		{
			set{ _organizationtypeid=value;}
			get{return _organizationtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizationTypeNo
		{
			set{ _organizationtypeno=value;}
			get{return _organizationtypeno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrganizationTypeName
		{
			set{ _organizationtypename=value;}
			get{return _organizationtypename;}
		}
		#endregion Model

	}
}

