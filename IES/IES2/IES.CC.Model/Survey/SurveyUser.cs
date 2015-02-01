/**  版本信息模板在安装目录下，可自行修改。
* SurveyUser.cs
*
* 功 能： N/A
* 类 名： SurveyUser
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/1/5 18:49:11   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.Survey
{
	/// <summary>
	/// 问卷参与用户
	/// </summary>
	[Serializable]
	public partial class SurveyUser
	{
		public SurveyUser()
		{}
		#region Model
		private int _surveyuserid;
		private int _surveyid;
		private int _userid;
		private int? _status;
		private int? _submittime;
		/// <summary>
		/// 
		/// </summary>
		public int SurveyUserID
		{
			set{ _surveyuserid=value;}
			get{return _surveyuserid;}
		}
		/// <summary>
		/// 问卷编号
		/// </summary>
		public int SurveyID
		{
			set{ _surveyid=value;}
			get{return _surveyid;}
		}
		/// <summary>
		/// 参与人编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 1 未提交；2已提交
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 最后保存时间
		/// </summary>
		public int? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		#endregion Model

	}
}

