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
        #region 补充信息

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileTitle { get; set; }

        /// <summary>
        /// 视频或文件的地址
        /// </summary>
        public string  ViewUrl { get; set; }

        public string DownloadUrl { get; set; }


        public int TimeLength { get; set; }

        public int  FileType { get; set; }

        public string Ext { get; set; }

        public DateTime UploadTime { get; set; }
        /// <summary>
        /// 视频学习进度 100 已经完成小于100 未完成
        /// </summary>
        public int FinishRate { get; set; }
        /// <summary>
        /// 视频需要学习百分之多少才算完成
        /// </summary>
        public int NeedRate { get; set; }

        public bool IsActive { get; set; }
        #endregion 



		public OCMoocFile()
		{
            this.IsActive = false;
        }
		#region Model
		private int _moocfileid;
		private int _ocid;
		private int _chapterid;
		private int _fileid;
		private int _timelimit=0;
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
		/// 是否必学资料
		/// </summary>
        public bool IsMust { get; set; }

        public int IsAllowStudy { get; set; }

        public int Seconds { get; set; }

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

