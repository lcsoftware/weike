﻿/**  版本信息模板在安装目录下，可自行修改。
* HoursComp.cs
*
* 功 能： N/A
* 类 名： HoursComp
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:34:57   N/A    初版
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
	/// 学时组成类型
	/// </summary>
	[Serializable]
	public partial class HoursComp
	{
		public HoursComp()
		{}
		#region Model
		private int _hourscompid;
		private string _hourscompno;
		private string _name;
		private int _orde;
		/// <summary>
		/// 
		/// </summary>
		public int HoursCompID
		{
			set{ _hourscompid=value;}
			get{return _hourscompid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HoursCompNo
		{
			set{ _hourscompno=value;}
			get{return _hourscompno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
