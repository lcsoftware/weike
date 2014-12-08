/**  版本信息模板在安装目录下，可自行修改。
* CourseHoursComp.cs
*
* 功 能： N/A
* 类 名： CourseHoursComp
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:34:56   N/A    初版
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
	/// 学时组成
	/// </summary>
	[Serializable]
	public partial class CourseHoursComp
	{
		public CourseHoursComp()
		{}
		#region Model
		private int _hourscompid;
		private string _hourscompno;
		private int _courseid;
		private int _teachingtypeid;
		private decimal _num;
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
		public int CourseID
		{
			set{ _courseid=value;}
			get{return _courseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TeachingTypeID
		{
			set{ _teachingtypeid=value;}
			get{return _teachingtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		#endregion Model

	}
}

