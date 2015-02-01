/**  版本信息模板在安装目录下，可自行修改。
* SurveyQuestion.cs
*
* 功 能： N/A
* 类 名： SurveyQuestion
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
	public partial class SurveyQuestion
	{
		public SurveyQuestion()
		{}
		#region Model
		private int _questionid;
		private int _surveyid;
		private int _type=1;
		private string _conten;
		private bool _ismust= true;
		private bool _israndom= false;
		private int _orde=1;
		/// <summary>
		/// 
		/// </summary>
		public int QuestionID
		{
			set{ _questionid=value;}
			get{return _questionid;}
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
		/// 1.单选题；  2多选题 ；  3 填空题\n4.打分题；  5排序题 ；  6 多项填空题\n7. 矩阵单选题 ； 8.矩阵多选题  ；9矩阵填空题\n10 矩阵打分题
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
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
		/// 该问题是否必须作答
		/// </summary>
		public bool IsMust
		{
			set{ _ismust=value;}
			get{return _ismust;}
		}
		/// <summary>
		/// 选项是否随机 1表示随机
		/// </summary>
		public bool IsRandom
		{
			set{ _israndom=value;}
			get{return _israndom;}
		}
		/// <summary>
		/// 问题的显示顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

