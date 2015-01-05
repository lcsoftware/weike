/**  版本信息模板在安装目录下，可自行修改。
* GroupMember.cs
*
* 功 能： N/A
* 类 名： GroupMember
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:57   N/A    初版
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
	/// 小组成员表
	/// </summary>
	[Serializable]
	public partial class GroupMember
	{
        #region 补充信息

        /// <summary>
        /// 小组成员姓名
        /// </summary>
        public string username { get; set; }


        #endregion 
		public GroupMember()
		{}
		#region Model
		private int _id;
		private int _groupid;
		private int _userid;
		private bool _isstudent= true;
		private int _role;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 分组编号
		/// </summary>
		public int GroupID
		{
			set{ _groupid=value;}
			get{return _groupid;}
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
		/// 1学生；0教师
		/// </summary>
		public bool IsStudent
		{
			set{ _isstudent=value;}
			get{return _isstudent;}
		}
		/// <summary>
		/// 1 小组长 ； 2 小组指导教师
		/// </summary>
		public int Role
		{
			set{ _role=value;}
			get{return _role;}
		}
		#endregion Model

	}
}

