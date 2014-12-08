/**  版本信息模板在安装目录下，可自行修改。
* PaperCardexercise.cs
*
* 功 能： N/A
* 类 名： PaperCardexercise
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:51   N/A    初版
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
	/// 答题卡习题
	/// </summary>
	[Serializable]
    public partial class PaperCardexercise  
	{
		public PaperCardexercise()
		{}
		#region Model
		private int _cardexerciseid;
		private int _paperid;
		private int _exercisetype;
		private int _choicenum;
		private string _answer;
		private decimal _score;
		private int _orde;
		/// <summary>
		/// 主键
		/// </summary>
		public int CardExerciseID
		{
			set{ _cardexerciseid=value;}
			get{return _cardexerciseid;}
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
		/// 1.判断；2单选；3多选；4填空；5简答题
		/// </summary>
		public int ExerciseType
		{
			set{ _exercisetype=value;}
			get{return _exercisetype;}
		}
		/// <summary>
		/// 选项的数量
		/// </summary>
		public int ChoiceNum
		{
			set{ _choicenum=value;}
			get{return _choicenum;}
		}
		/// <summary>
		/// 答案.
        ///单选题答案  : 3  表示第三个选项是正确答案
        ///多选题答案  : 2able2004acc20053   表示第2,3是正确答案,答案分隔符号able2004acc2005
        ///填空题答案：北京able2004acc2005上海  表示第一个填空答案是北京 第二题的填空是上海 分隔符号able2004acc2005
		/// </summary>
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
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
		/// 习题的顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

