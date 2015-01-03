/**  版本信息模板在安装目录下，可自行修改。
* GroupFileType.cs
*
* 功 能： N/A
* 类 名： GroupFileType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:57   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 课程需要上传的文件类型表
	/// </summary>
	[Serializable]
	public partial class GroupFileType
	{
		public GroupFileType()
		{}
		#region Model
		private int _groupfiletypeid;
		private int _ocid=0;
		private string _filetypename;
		private string _fileextname;
		/// <summary>
		/// 
		/// </summary>
		public int GroupFileTypeID
		{
			set{ _groupfiletypeid=value;}
			get{return _groupfiletypeid;}
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
		public string FileTypeName
		{
			set{ _filetypename=value;}
			get{return _filetypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FileExtName
		{
			set{ _fileextname=value;}
			get{return _fileextname;}
		}
		#endregion Model

	}
}

