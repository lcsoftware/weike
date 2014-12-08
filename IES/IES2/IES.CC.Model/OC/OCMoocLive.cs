/**  版本信息模板在安装目录下，可自行修改。
* OCMoocLive.cs
*
* 功 能： N/A
* 类 名： OCMoocLive
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:26   N/A    初版
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
	/// Mooc互动
	/// </summary>
	[Serializable]
	public partial class OCMoocLive
	{
		public OCMoocLive()
		{}
		#region Model
		private int _moocliveid;
		private int _ocid;
		private int _chapterid;
		private int _sourceid=0;
		private string _source;
		private int _orde=1;
		/// <summary>
		/// 
		/// </summary>
		public int MoocLiveID
		{
			set{ _moocliveid=value;}
			get{return _moocliveid;}
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
		/// 0 表示结业测试
		/// </summary>
		public int ChapterID
		{
			set{ _chapterid=value;}
			get{return _chapterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// ForumTopic 表示论题 ； Test 表示作业或考试。。。
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

