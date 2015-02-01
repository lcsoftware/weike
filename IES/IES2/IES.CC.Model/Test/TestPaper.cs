/**  版本信息模板在安装目录下，可自行修改。
* TestPaper.cs
*
* 功 能： N/A
* 类 名： TestPaper
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:47   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Test.Model
{
	/// <summary>
	/// 测试用试卷
	/// </summary>
	[Serializable]
	public partial class TestPaper
	{
		public TestPaper()
		{}
		#region Model
		private int _testtacticid;
		private int _testid;
		private int _paperid;
		/// <summary>
		/// 
		/// </summary>
		public int TestTacticID
		{
			set{ _testtacticid=value;}
			get{return _testtacticid;}
		}
		/// <summary>
		/// 作业考试编号
		/// </summary>
		public int TestID
		{
			set{ _testid=value;}
			get{return _testid;}
		}
		/// <summary>
		/// 本次考试使用的试卷编号
		/// </summary>
		public int PaperID
		{
			set{ _paperid=value;}
			get{return _paperid;}
		}
		#endregion Model

	}
}

