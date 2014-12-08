/**  版本信息模板在安装目录下，可自行修改。
* OCMooc.cs
*
* 功 能： N/A
* 类 名： OCMooc
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:25   N/A    初版
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
	/// mooc 课程的基本参数设置
	/// </summary>
	[Serializable]
	public partial class OCMooc
	{
		public OCMooc()
		{}
		#region Model
		private int _moocid;
		private int _ocid;
		private bool _iscross= false;
		private bool _isstudentcheck= false;
		private bool _isdrag= false;
		private bool _isshowfirstchapter= false;
		private int? _poptime=0;
		/// <summary>
		/// 
		/// </summary>
		public int MoocID
		{
			set{ _moocid=value;}
			get{return _moocid;}
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
		/// 1允许夸章学习 ，0按章节顺序进行学习
		/// </summary>
		public bool IsCross
		{
			set{ _iscross=value;}
			get{return _iscross;}
		}
		/// <summary>
		/// 是否学生相互评阅作业
		/// </summary>
		public bool IsStudentCheck
		{
			set{ _isstudentcheck=value;}
			get{return _isstudentcheck;}
		}
		/// <summary>
		/// 是否允许视频拖拽观看
		/// </summary>
		public bool IsDrag
		{
			set{ _isdrag=value;}
			get{return _isdrag;}
		}
		/// <summary>
		/// 是否显示绪论
		/// </summary>
		public bool IsShowFirstChapter
		{
			set{ _isshowfirstchapter=value;}
			get{return _isshowfirstchapter;}
		}
		/// <summary>
		/// 间隔多少时间弹出学习提示,0 表示不弹出提示
		/// </summary>
		public int? PopTime
		{
			set{ _poptime=value;}
			get{return _poptime;}
		}
		#endregion Model

	}
}

