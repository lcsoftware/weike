/**  版本信息模板在安装目录下，可自行修改。
* OCFCLive.cs
*
* 功 能： N/A
* 类 名： OCFCLive
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:24   N/A    初版
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
	/// 翻转课堂互动任务
	/// </summary>
	[Serializable]
	public partial class OCFCLive
	{
		public OCFCLive()
		{}
		#region Model
		private int _fcliveid;
		private int _ocid;
		private int _sourceid;
		private string _source;
		private int _orde=1;
		private bool _isgroup= false;
		private int? _evascore=0;
		/// <summary>
		/// 
		/// </summary>
		public int FCLiveID
		{
			set{ _fcliveid=value;}
			get{return _fcliveid;}
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
		public int SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
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
		/// <summary>
		/// 该互动是否需要分组
		/// </summary>
		public bool IsGroup
		{
			set{ _isgroup=value;}
			get{return _isgroup;}
		}
		/// <summary>
		/// 作业评分
		/// </summary>
		public int? EvaScore
		{
			set{ _evascore=value;}
			get{return _evascore;}
		}
		#endregion Model

	}
}

