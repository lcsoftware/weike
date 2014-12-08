/**  版本信息模板在安装目录下，可自行修改。
* OCMoocFile.cs
*
* 功 能： N/A
* 类 名： OCMoocFile
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
	/// mooc资源
	/// </summary>
	[Serializable]
	public partial class OCMoocFile
	{
		public OCMoocFile()
		{}
		#region Model
		private int _moocfileid;
		private int _ocid;
		private int _chapterid;
		private int _fileid;
		private int _timelimit=0;
		private string _brief;
		private bool _ismust= true;
		private int _planday=0;
		private int _minhour=0;
		private int _orde=1;
		/// <summary>
		/// 
		/// </summary>
		public int MoocFileID
		{
			set{ _moocfileid=value;}
			get{return _moocfileid;}
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
		public int ChapterID
		{
			set{ _chapterid=value;}
			get{return _chapterid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FileID
		{
			set{ _fileid=value;}
			get{return _fileid;}
		}
		/// <summary>
		/// 学习时长 秒为单位
		/// </summary>
		public int Timelimit
		{
			set{ _timelimit=value;}
			get{return _timelimit;}
		}
		/// <summary>
		/// 本章节的描述信息
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 是否必学资料
		/// </summary>
		public bool IsMust
		{
			set{ _ismust=value;}
			get{return _ismust;}
		}
		/// <summary>
		/// 计划天数
		/// </summary>
		public int PlanDay
		{
			set{ _planday=value;}
			get{return _planday;}
		}
		/// <summary>
		/// 最低学时
		/// </summary>
		public int MinHour
		{
			set{ _minhour=value;}
			get{return _minhour;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

