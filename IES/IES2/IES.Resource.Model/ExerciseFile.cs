/**  版本信息模板在安装目录下，可自行修改。
* ExerciseFile.cs
*
* 功 能： N/A
* 类 名： ExerciseFile
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:48   N/A    初版
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
	/// 习题的附件
	/// </summary>
	[Serializable]
	public partial class ExerciseFile
	{
		public ExerciseFile()
		{}
		#region Model
		private int _id;
		private int _exerciseid=0;
		private int _attachmentid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 习题的编号
		/// </summary>
		public int ExerciseID
		{
			set{ _exerciseid=value;}
			get{return _exerciseid;}
		}
		/// <summary>
		/// 附件表的编号
		/// </summary>
		public int AttachmentID
		{
			set{ _attachmentid=value;}
			get{return _attachmentid;}
		}
		#endregion Model

	}
}

