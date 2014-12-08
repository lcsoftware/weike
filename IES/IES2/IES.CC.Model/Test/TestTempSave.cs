/**  版本信息模板在安装目录下，可自行修改。
* TestTempSave.cs
*
* 功 能： N/A
* 类 名： TestTempSave
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:48   N/A    初版
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
	public partial class TestTempSave
	{
		public TestTempSave()
		{}
		#region Model
		private int _id;
		private int _testid;
		private int _userid;
		private string _answer;
		private DateTime? _savetime= DateTime.Now;
		private bool _isfinish= false;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 测试编号
		/// </summary>
		public int TestID
		{
			set{ _testid=value;}
			get{return _testid;}
		}
		/// <summary>
		/// 用户编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 答案
		/// </summary>
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		/// <summary>
		/// 最后保存时间
		/// </summary>
		public DateTime? SaveTime
		{
			set{ _savetime=value;}
			get{return _savetime;}
		}
		/// <summary>
		/// 是否已经提交
		/// </summary>
		public bool IsFinish
		{
			set{ _isfinish=value;}
			get{return _isfinish;}
		}
		#endregion Model

	}
}

