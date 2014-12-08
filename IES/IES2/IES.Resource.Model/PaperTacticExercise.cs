/**  版本信息模板在安装目录下，可自行修改。
* PaperTacticExercise.cs
*
* 功 能： N/A
* 类 名： PaperTacticExercise
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:53   N/A    初版
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
	/// 试卷策略下所有满足条件的习题，供快速出题用
	/// </summary>
	[Serializable]
    public partial class PaperTacticExercise 
	{
		public PaperTacticExercise()
		{}
		#region Model
		private int _papertacticexerciseid;
		private int _papertacticid;
		private int _groupid;
		private int _exerciseid;
		private int _parentexerciseid;
		/// <summary>
		/// 
		/// </summary>
		public int PaperTacticExerciseID
		{
			set{ _papertacticexerciseid=value;}
			get{return _papertacticexerciseid;}
		}
		/// <summary>
		/// 试卷策略编号
		/// </summary>
		public int PaperTacticID
		{
			set{ _papertacticid=value;}
			get{return _papertacticid;}
		}
		/// <summary>
		/// 试卷分组编号
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
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
		/// 组合题父题的编号
		/// </summary>
		public int ParentExerciseID
		{
			set{ _parentexerciseid=value;}
			get{return _parentexerciseid;}
		}
		#endregion Model

	}
}

