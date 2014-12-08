/**  版本信息模板在安装目录下，可自行修改。
* TeachingClassTeacher.cs
*
* 功 能： N/A
* 类 名： TeachingClassTeacher
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:04   N/A    初版
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
	/// 教学班授课教师
	/// </summary>
	[Serializable]
	public partial class TeachingClassTeacher
	{
		public TeachingClassTeacher()
		{}
		#region Model
		private int _id;
		private int _teachingclassid;
		private int _userid;
		private bool _ismainteacher= true;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 教学班ID
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		/// <summary>
		/// 授课教师编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 主讲教师
		/// </summary>
		public bool IsMainTeacher
		{
			set{ _ismainteacher=value;}
			get{return _ismainteacher;}
		}
		#endregion Model

	}
}

