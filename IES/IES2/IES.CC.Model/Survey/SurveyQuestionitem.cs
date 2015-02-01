/**  版本信息模板在安装目录下，可自行修改。
* SurveyQuestionitem.cs
*
* 功 能： N/A
* 类 名： SurveyQuestionitem
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
	/// 问卷发放对象
	/// </summary>
	[Serializable]
	public partial class SurveyQuestionitem
	{
		public SurveyQuestionitem()
		{}
		#region Model
		private int _questionitemid;
		private int _questionid;
		private string _conten;
		private bool _isrowcolumn= true;
		private int _maxscore;
		private int _toquestionid=0;
		private int _orde=1;
		private int? _minsel=0;
		private int? _maxsel=0;
		/// <summary>
		/// 
		/// </summary>
		public int QuestionItemID
		{
			set{ _questionitemid=value;}
			get{return _questionitemid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int QuestionID
		{
			set{ _questionid=value;}
			get{return _questionid;}
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
		/// 1行  ； 0 列
		/// </summary>
		public bool IsRowColumn
		{
			set{ _isrowcolumn=value;}
			get{return _isrowcolumn;}
		}
		/// <summary>
		/// 分值
		/// </summary>
		public int MaxScore
		{
			set{ _maxscore=value;}
			get{return _maxscore;}
		}
		/// <summary>
		/// 选中该选项则跳转到第几题
		/// </summary>
		public int ToQuestionID
		{
			set{ _toquestionid=value;}
			get{return _toquestionid;}
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
		/// 最小选择数量
		/// </summary>
		public int? MinSel
		{
			set{ _minsel=value;}
			get{return _minsel;}
		}
		/// <summary>
		/// 最多选择数量
		/// </summary>
		public int? MaxSel
		{
			set{ _maxsel=value;}
			get{return _maxsel;}
		}
		#endregion Model

	}
}

