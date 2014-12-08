/**  版本信息模板在安装目录下，可自行修改。
* FileVideoIndex.cs
*
* 功 能： N/A
* 类 名： FileVideoIndex
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:50   N/A    初版
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
	/// 视频关键点索引
	/// </summary>
	[Serializable]
	public partial class FileVideoIndex
	{
		public FileVideoIndex()
		{}
		#region Model
		private int _videoindexid;
		private int _fileid;
		private string _keys;
		private int _startsecond;
		private int _endsecond;
		/// <summary>
		/// 主键
		/// </summary>
		public int VideoIndexID
		{
			set{ _videoindexid=value;}
			get{return _videoindexid;}
		}
		/// <summary>
		/// 视频文件编号
		/// </summary>
		public int FileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 知识点
		/// </summary>
		public string Keys
		{
			set{ _keys=value;}
			get{return _keys;}
		}
		/// <summary>
		/// 开始时间秒
		/// </summary>
		public int StartSecond
		{
			set{ _startsecond=value;}
			get{return _startsecond;}
		}
		/// <summary>
		/// 结束时间秒
		/// </summary>
		public int EndSecond
		{
			set{ _endsecond=value;}
			get{return _endsecond;}
		}
		#endregion Model

	}
}

