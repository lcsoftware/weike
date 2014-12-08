/**  版本信息模板在安装目录下，可自行修改。
* FileKen.cs
*
* 功 能： N/A
* 类 名： FileKen
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:49   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.Resource.Model
{
	/// <summary>
	/// 文件与知识点的关联
	/// </summary>
	[Serializable]
	public partial class FileKen
	{
		public FileKen()
		{}
		#region Model
		private int _id;
		private int _fileid;
		private int _kenid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 文件编号
		/// </summary>
		public int FileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 知识点编号
		/// </summary>
		public int KenID
		{
			set{ _kenid=value;}
			get{return _kenid;}
		}
		#endregion Model

	}
}

