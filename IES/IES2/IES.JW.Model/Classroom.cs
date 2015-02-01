/**  版本信息模板在安装目录下，可自行修改。
* Classroom.cs
*
* 功 能： N/A
* 类 名： Classroom
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:30   N/A    初版
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
	/// 教室
	/// </summary>
	[Serializable]
	public partial class Classroom
	{
		public Classroom()
		{}
		#region Model
		private int _classroomid;
		private string _classroomno;
		private string _classroomname;
		private int _teachingbuildingid=0;
		private string _classroomtype;
		private int _floor=1;
		private int _studentsnumber=0;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int ClassroomID
		{
			set{ _classroomid=value;}
			get{return _classroomid;}
		}
		/// <summary>
		/// 教室代码，必须，唯一
		/// </summary>
		public string ClassroomNo
		{
			set{ _classroomno=value;}
			get{return _classroomno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClassroomName
		{
			set{ _classroomname=value;}
			get{return _classroomname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TeachingBuildingID
		{
			set{ _teachingbuildingid=value;}
			get{return _teachingbuildingid;}
		}
		/// <summary>
		/// 教室类型名称
		/// </summary>
		public string ClassroomType
		{
			set{ _classroomtype=value;}
			get{return _classroomtype;}
		}
		/// <summary>
		/// 所在楼层
		/// </summary>
		public int Floor
		{
			set{ _floor=value;}
			get{return _floor;}
		}
		/// <summary>
		/// 授课人数
		/// </summary>
		public int StudentsNumber
		{
			set{ _studentsnumber=value;}
			get{return _studentsnumber;}
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

