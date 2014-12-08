/**  版本信息模板在安装目录下，可自行修改。
* PaperGroupFile.cs
*
* 功 能： N/A
* 类 名： PaperGroupFile
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:52   N/A    初版
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
	/// 试卷分组附件
	/// </summary>
	[Serializable]
    public partial class PaperGroupFile 
	{
		public PaperGroupFile()
		{}
		#region Model
		private int _id;
		private int _groupid;
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
		/// 分组编号
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 附件编号
		/// </summary>
		public int AttachmentID
		{
			set{ _attachmentid=value;}
			get{return _attachmentid;}
		}
		#endregion Model

	}
}

