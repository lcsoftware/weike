/**  版本信息模板在安装目录下，可自行修改。
* ExerciseChoice.cs
*
* 功 能： N/A
* 类 名： ExerciseChoice
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
	/// 习题选择项
	/// </summary>
	[Serializable]
	public partial class ExerciseChoice
	{
		public ExerciseChoice()
		{}
		#region Model
		private int _choiceid;
		private int _exerciseid;
		private string _conten;
		private bool _iscorrect= false;
		private int? _grou=0;
		private int? _ordernum=0;
		private bool _isdeleted= false;
        private string _answer;
		/// <summary>
		/// 主键 选项编号
		/// </summary>
		public int ChoiceID
		{
			set{ _choiceid=value;}
			get{return _choiceid;}
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
		/// 选择题、判断题的选项内容；填空题的答案填空题多个答案用able2004acc2005分隔
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 是否正确选项（选择题、判断题）
		/// </summary>
		public bool IsCorrect
		{
			set{ _iscorrect=value;}
			get{return _iscorrect;}
		}
		/// <summary>
		/// 连线题分组用,连线题分组编号
		/// </summary>
		public int? Grou
		{
			set{ _grou=value;}
			get{return _grou;}
		}
		/// <summary>
		/// 排序题，排序值 。
        ///连线题的排序号一致 表示正确
        ///填空题的习题答案顺序。1为首选答案，其他为备选答案。
		/// </summary>
		public int? OrderNum
		{
			set{ _ordernum=value;}
			get{return _ordernum;}
		}
		/// <summary>
		/// 删除状态
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}

        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
		#endregion Model

	}
}

