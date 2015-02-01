/**  版本信息模板在安装目录下，可自行修改。
* PaperGroup.cs
*
* 功 能： N/A
* 类 名： PaperGroup
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
	/// 试卷习题分组
	/// </summary>
	[Serializable]
    public partial class PaperGroup
    {
        #region  补充属性
        /// <summary>
        /// 分组 习题总数  （此属性用于页面展示，不存库）
        /// </summary>
        public int ExerciseCount { get; set; }

        /// <summary>
        /// 分组 习题总分数  （此属性用于页面展示，不存库）
        /// </summary>
        public int ExerciseScore { get; set; }
        #endregion

        public PaperGroup()
		{}
		#region Model
		private int _groupid;
		private int _paperid;
		private string _groupname;
		private int _orde=1;
		private string _brief;
		private int _timelimit=0;
		/// <summary>
		/// 
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 试卷编号
		/// </summary>
		public int PaperID
		{
			set{ _paperid=value;}
			get{return _paperid;}
		}
		/// <summary>
		/// 分组名称
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 分组排序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		/// <summary>
		/// 分组的注释说明
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 分组限时分钟 0表示不限制
		/// </summary>
		public int Timelimit
		{
			set{ _timelimit=value;}
			get{return _timelimit;}
		}
		#endregion Model

	}
}

