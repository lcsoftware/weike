/**  版本信息模板在安装目录下，可自行修改。
* SurveyAnswer.cs
*
* 功 能： N/A
* 类 名： SurveyAnswer
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/1/5 18:49:10   N/A    初版
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
	/// 问卷调查
	/// </summary>
	[Serializable]
	public partial class SurveyAnswer
	{
		public SurveyAnswer()
		{}
		#region Model
		private int _answerid;
		private int _surveyid;
		private int _questionid;
		private int _userid;
		private string _conten;
		private int _questionrowid;
		private int _questioncolumnid;
		private int _score=0;
		/// <summary>
		/// 
		/// </summary>
		public int AnswerID
		{
			set{ _answerid=value;}
			get{return _answerid;}
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
		/// 问题编号
		/// </summary>
		public int QuestionID
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// 答题人编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int QuestionRowID
		{
			set{ _questionrowid=value;}
			get{return _questionrowid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int QuestionColumnID
		{
			set{ _questioncolumnid=value;}
			get{return _questioncolumnid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		#endregion Model

	}
}

