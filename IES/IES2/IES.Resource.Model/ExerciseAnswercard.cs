/**  版本信息模板在安装目录下，可自行修改。
* ExerciseAnswercard.cs
*
* 功 能： N/A
* 类 名： ExerciseAnswercard
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:48   N/A    初版
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
	/// 习题答题卡表
	/// </summary>
	[Serializable]
	public partial class ExerciseAnswercard
	{
		public ExerciseAnswercard()
		{}
		#region Model
		private int _exerciseanswercardid;
		private int _exerciseid;
		private string _correctanswer;
		private decimal _score=0M;
		private int _type=2;
		private int _choicenum=4;
		/// <summary>
		/// 
		/// </summary>
		public int ExerciseAnswercardID
		{
			set{ _exerciseanswercardid=value;}
			get{return _exerciseanswercardid;}
		}
		/// <summary>
		/// 习题编号
		/// </summary>
		public int ExerciseID
		{
			set{ _exerciseid=value;}
			get{return _exerciseid;}
		}
		/// <summary>
		/// 答题卡每个习题的正确答案，单选题的答案如下1表示A ，4表示D；多选题2able2004acc20053表示B,C
		/// </summary>
		public string CorrectAnswer
		{
			set{ _correctanswer=value;}
			get{return _correctanswer;}
		}
		/// <summary>
		/// 分值
		/// </summary>
		public decimal Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		///  '1判断题；2 单选题 ；  3 多选题 ； 4填空题 ； 5 简答题'
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 客观题选项数量
		/// </summary>
		public int ChoiceNum
		{
			set{ _choicenum=value;}
			get{return _choicenum;}
		}
		#endregion Model

	}
}

