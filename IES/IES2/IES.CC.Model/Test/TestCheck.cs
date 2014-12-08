/**  版本信息模板在安装目录下，可自行修改。
* TestCheck.cs
*
* 功 能： N/A
* 类 名： TestCheck
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:46   N/A    初版
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
	/// 流水批阅设置
	/// </summary>
	[Serializable]
	public partial class TestCheck
	{
		public TestCheck()
		{}
		#region Model
		private int _testcheckid;
		private int _testid;
		private int _userid;
		private int _objid;
		private bool _type= true;
		private bool _status;
		private DateTime _updatetime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int TestCheckID
		{
			set{ _testcheckid=value;}
			get{return _testcheckid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TestID
		{
			set{ _testid=value;}
			get{return _testid;}
		}
		/// <summary>
		/// 助教编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 习题编号或者教学班编号
		/// </summary>
		public int ObjID
		{
			set{ _objid=value;}
			get{return _objid;}
		}
		/// <summary>
		/// 1 按习题分  ； 2  按教学班 
		/// </summary>
		public bool Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 批阅状态 0 未完成；   1完成批阅
		/// </summary>
		public bool Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

