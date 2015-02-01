/**  版本信息模板在安装目录下，可自行修改。
* PaperExercise.cs
*
* 功 能： N/A
* 类 名： PaperExercise
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
using System.Collections.Generic;
namespace IES.Resource.Model
{
	/// <summary>
	/// 试卷下的习题
	/// </summary>
	[Serializable]
    public partial class PaperExercise
    {

        #region 补充信息
      //  t2.Conten, t2.Kens, t2.Keys , t2.Diffcult,t2.IsRand , t2.ExerciseTypeName , t2.ExerciseType

        /// <summary>
        /// 习题内容
        /// </summary>
        public string Conten { get; set;  }

        /// <summary>
        /// 知识点
        /// </summary>
        public string Kens { get; set; }


        /// <summary>
        /// 关键字
        /// </summary>
        public string Keys { get; set; }


        /// <summary>
        /// 难度系统
        /// </summary>
        public int Diffcult { get; set; }

        /// <summary>
        /// 是否随机
        /// </summary>
        public bool IsRand { get; set; }

        /// <summary>
        /// 题型名称
        /// </summary>
        public string ExerciseTypeName { get; set; }


        /// <summary>
        /// 题型
        /// </summary>
        public int ExerciseType { get; set; }

        public List<ExerciseChoice> ExerciseChoices { get; set; }

        #endregion 

        public PaperExercise()
		{
            this.ExerciseChoices = new List<ExerciseChoice>();
        }
		#region Model
		private int _paperexerciseid;
		private int _paperid;
		private int _papergroupid;
		private int _exerciseid=0;
		private int _parentexerciseid=0;
		private int? _papertacticid=0;
		private decimal _score;
		private int _orde;
		/// <summary>
		/// 主键
		/// </summary>
		public int PaperExerciseID
		{
			set{ _paperexerciseid=value;}
			get{return _paperexerciseid;}
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
		/// 试卷分组
		/// </summary>
		public int PaperGroupID
		{
			set{ _papergroupid=value;}
			get{return _papergroupid;}
		}
		/// <summary>
		/// 习题编号，如果为0，则PaperTacticID
		/// </summary>
		public int ExerciseID
		{
			set{ _exerciseid=value;}
			get{return _exerciseid;}
		}
		/// <summary>
		/// 组合题父题的编号
		/// </summary>
		public int ParentExerciseID
		{
			set{ _parentexerciseid=value;}
			get{return _parentexerciseid;}
		}
		/// <summary>
		/// 组卷策略的编号，如果ExerciseID为0，则PaperTacticID>0
		/// </summary>
		public int? PaperTacticID
		{
			set{ _papertacticid=value;}
			get{return _papertacticid;}
		}
		/// <summary>
		/// 习题分值
		/// </summary>
		public decimal Score
		{
			set{ _score=value;}
			get{return _score;}
		}
		/// <summary>
		/// 习题顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		#endregion Model

	}
}

