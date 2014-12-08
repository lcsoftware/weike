/**  版本信息模板在安装目录下，可自行修改。
* OCTeam.cs
*
* 功 能： N/A
* 类 名： OCTeam
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:31   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.OC.Model
{
	/// <summary>
	/// 我的课程团队，课程主讲教师
	/// </summary>
	[Serializable]
	public partial class OCTeam
	{
		public OCTeam()
		{}
		#region Model
		private int _teamid;
		private int _ocid;
		private int _userid;
		private int? _owneruserid=0;
		private int _role=0;
		private string _brief;
		private int? _status=0;
		private DateTime? _applydate= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public int TeamID
		{
			set{ _teamid=value;}
			get{return _teamid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 属于哪个用户的助教，默认是课程负责人的助教
		/// </summary>
		public int? OwnerUserID
		{
			set{ _owneruserid=value;}
			get{return _owneruserid;}
		}
		/// <summary>
		/// 0 课程创建人、 1 课程负责人 ;  2 主讲教师  ;  3 助教（需对功能模块授权） ；4 教学督导（该用户不体现在教学团队中，系统默认创建，教学督导有对资源建设、互动的浏览权限）。
		/// </summary>
		public int Role
		{
			set{ _role=value;}
			get{return _role;}
		}
		/// <summary>
		/// 简介
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 审核状态： 0 待审核，1审核未通过 ；2审核通过
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 申请时间
		/// </summary>
		public DateTime? ApplyDate
		{
			set{ _applydate=value;}
			get{return _applydate;}
		}
		#endregion Model

	}
}

