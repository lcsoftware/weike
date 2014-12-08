/**  版本信息模板在安装目录下，可自行修改。
* OCClass.cs
*
* 功 能： N/A
* 类 名： OCClass
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
	/// 我建设的在线课程
	/// </summary>
	[Serializable]
	public partial class OCClass
	{
		public OCClass()
		{}
		#region Model
		private int _occlassid;
		private int _ocid;
		private int _teachingclassid;
		private bool _isjwclass= true;
		private bool _ismoocclass= false;
		private bool _isfcclass= false;
		/// <summary>
		/// 
		/// </summary>
		public int OCClassID
		{
			set{ _occlassid=value;}
			get{return _occlassid;}
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
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		/// <summary>
		/// 是否教务系统的教学班
		/// </summary>
		public bool IsJWClass
		{
			set{ _isjwclass=value;}
			get{return _isjwclass;}
		}
		/// <summary>
		/// MOOC教学班
		/// </summary>
		public bool IsMoocClass
		{
			set{ _ismoocclass=value;}
			get{return _ismoocclass;}
		}
		/// <summary>
		/// 翻转课堂教学班
		/// </summary>
		public bool IsFCClass
		{
			set{ _isfcclass=value;}
			get{return _isfcclass;}
		}
		#endregion Model

	}
}

