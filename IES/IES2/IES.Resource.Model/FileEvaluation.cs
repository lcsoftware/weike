/**  版本信息模板在安装目录下，可自行修改。
* FileEvaluation.cs
*
* 功 能： N/A
* 类 名： FileEvaluation
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:49   N/A    初版
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
	/// 文件整体评价
	/// </summary>
	[Serializable]
	public partial class FileEvaluation
	{
		public FileEvaluation()
		{}
		#region Model
		private int _fileevaluationid;
		private int _fileid;
		private int _userid;
		private int _score=-1;
		private string _brief;
		private DateTime? _updatetime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int FileEvaluationID
		{
			set{ _fileevaluationid=value;}
			get{return _fileevaluationid;}
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
		/// 评价人编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 得分 5,4,3,2,1  共5等级
		/// </summary>
		public int Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 简要评价
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

