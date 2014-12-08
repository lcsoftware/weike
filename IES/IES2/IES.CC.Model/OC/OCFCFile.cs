/**  版本信息模板在安装目录下，可自行修改。
* OCFCFile.cs
*
* 功 能： N/A
* 类 名： OCFCFile
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:23   N/A    初版
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
	/// 翻转课堂资料学习表
	/// </summary>
	[Serializable]
	public partial class OCFCFile
	{
		public OCFCFile()
		{}
		#region Model
		private int _fcfileid;
		private int _fcid;
		private int _fileid;
		private int _userid;
		private bool _ismust= false;
		private int _time=0;
		private int _orde=1;
		/// <summary>
		/// 
		/// </summary>
		public int FCFileID
		{
			set{ _fcfileid=value;}
			get{return _fcfileid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FCID
		{
			set{ _fcid=value;}
			get{return _fcid;}
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
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 是否是必学资料，必学资料会在学习进度中显示
		/// </summary>
		public bool IsMust
		{
			set{ _ismust=value;}
			get{return _ismust;}
		}
		/// <summary>
		/// 学习时长（分钟）,0表示不限制
		/// </summary>
		public int Time
		{
			set{ _time=value;}
			get{return _time;}
		}
		/// <summary>
		/// 显示顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

