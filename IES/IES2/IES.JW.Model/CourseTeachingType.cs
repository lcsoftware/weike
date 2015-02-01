/**  版本信息模板在安装目录下，可自行修改。
* CourseTeachingType.cs
*
* 功 能： N/A
* 类 名： CourseTeachingType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:31   N/A    初版
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
	/// 授课方式
	/// </summary>
	[Serializable]
	public partial class CourseTeachingType
	{
		public CourseTeachingType()
		{}
		#region Model
		private int _teachingtypeid;
		private string _teachingtypeno;
		private string _name;
		private int _orde=1;
		private bool _isdeleted= false;
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
		public string TeachingTypeNo
		{
			set{ _teachingtypeno=value;}
			get{return _teachingtypeno;}
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

