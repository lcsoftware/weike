/**  版本信息模板在安装目录下，可自行修改。
* GroupTaskGroup.cs
*
* 功 能： N/A
* 类 名： GroupTaskGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:59   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 小组任务表
	/// </summary>
	[Serializable]
	public partial class GroupTaskGroup
	{
		public GroupTaskGroup()
		{}
		#region Model
		private int _id;
		private int _taskid;
		private int _groupid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 教学任务编号
		/// </summary>
		public int TaskID
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 小组编号
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		#endregion Model

	}
}

