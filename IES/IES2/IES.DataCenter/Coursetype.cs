/**  版本信息模板在安装目录下，可自行修改。
* Coursetype.cs
*
* 功 能： N/A
* 类 名： Coursetype
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
	/// 课程分类
	/// </summary>
	[Serializable]
	public partial class Coursetype
	{
		public Coursetype()
		{}
		#region Model
		private int _coursetypeid;
		private string _coursetypeno;
		private string _name;
		private int _parentid=0;
		private int _order=1;
		/// <summary>
		/// 
		/// </summary>
		public int CourseTypeID
		{
			set{ _coursetypeid=value;}
			get{return _coursetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CourseTypeNo
		{
			set{ _coursetypeno=value;}
			get{return _coursetypeno;}
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
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order
		{
			set{ _order=value;}
			get{return _order;}
		}
		#endregion Model

	}
}

