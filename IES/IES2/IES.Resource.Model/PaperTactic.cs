/**  版本信息模板在安装目录下，可自行修改。
* PaperTactic.cs
*
* 功 能： N/A
* 类 名： PaperTactic
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
	/// 试卷组卷策略
	/// </summary>
	[Serializable]
	public partial class PaperTactic 
	{
		public PaperTactic()
		{}
		#region Model
		private int _papertacticid;
		private int _paperid;
		private int _groupid;
		private int _exercisetype;
		private string _num;
		private decimal _scoreper;
		private int _kenid;
		/// <summary>
		/// 
		/// </summary>
		public int PaperTacticID
		{
			set{ _papertacticid=value;}
			get{return _papertacticid;}
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
		/// 习题所在的分组
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 习题题型
		/// </summary>
		public int ExerciseType
		{
			set{ _exercisetype=value;}
			get{return _exercisetype;}
		}
		/// <summary>
		/// 抽题数量E:20,N:10,H:5,S:35表达的意思是简单20题 中等10题 难5题 总数35题
		/// </summary>
		public string Num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 单个习题的分值
		/// </summary>
		public decimal ScorePer
		{
			set{ _scoreper=value;}
			get{return _scoreper;}
		}
		/// <summary>
		/// 知识点编号
		/// </summary>
		public int KenID
		{
			set{ _kenid=value;}
			get{return _kenid;}
		}
		#endregion Model

	}
}

