/**  版本信息模板在安装目录下，可自行修改。
* GroupResult.cs
*
* 功 能： N/A
* 类 名： GroupResult
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:58   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 分组模式表
	/// </summary>
	[Serializable]
	public partial class GroupResult
	{
		public GroupResult()
		{}


        #region 补充信息

        /// <summary>
        /// 用户姓名
        /// </summary>
        public int username { get; set; }

        /// <summary>
        /// 小组名称
        /// </summary>
        public int name { get; set; }

        #endregion 

		#region Model
		private int _resultid;
		private int _taskid;
		private int _groupid;
		private int _userid;
		private string _filename;
		private DateTime _submittime= DateTime.Now;
		private int _clicks=0;
		private int _groupfiletypeid=-1;
		private string _score;
		private int? _scoretype=1;
		/// <summary>
		/// 成果编号
		/// </summary>
		public int ResultID
		{
			set{ _resultid=value;}
			get{return _resultid;}
		}
		/// <summary>
		/// 教学任务编号
		/// </summary>
		public int TaskID
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 小组编号
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 上传用户编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 成果题目
		/// </summary>
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 上传时间
		/// </summary>
		public DateTime Submittime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		/// <summary>
		/// 查看次数
		/// </summary>
		public int Clicks
		{
			set{ _clicks=value;}
			get{return _clicks;}
		}
		/// <summary>
		/// 成果文件类型编号
		/// </summary>
		public int GroupFileTypeID
		{
			set{ _groupfiletypeid=value;}
			get{return _groupfiletypeid;}
		}
		/// <summary>
		/// 得分
		/// </summary>
		public string Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 成绩评定类型 1
		/// </summary>
		public int? ScoreType
		{
			set{ _scoretype=value;}
			get{return _scoretype;}
		}
		#endregion Model

	}
}

