/**  版本信息模板在安装目录下，可自行修改。
* OCFCGroupUser.cs
*
* 功 能： N/A
* 类 名： OCFCGroupUser
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:24   N/A    初版
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
	/// 翻转课堂分组详情
	/// </summary>
	[Serializable]
	public partial class OCFCGroupUser
	{
		public OCFCGroupUser()
		{}
		#region Model
		private int _fcgroupuserid;
		private int _ocfcid;
		private int _fcgroupid;
		private int _groupnum;
		private int _userid;
		private int _userrole=0;
		/// <summary>
		/// 
		/// </summary>
		public int FCGroupUserID
		{
			set{ _fcgroupuserid=value;}
			get{return _fcgroupuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCFCID
		{
			set{ _ocfcid=value;}
			get{return _ocfcid;}
		}
		/// <summary>
		/// 分组编号
		/// </summary>
		public int FCGroupID
		{
			set{ _fcgroupid=value;}
			get{return _fcgroupid;}
		}
		/// <summary>
		/// 分组编号
		/// </summary>
		public int GroupNum
		{
			set{ _groupnum=value;}
			get{return _groupnum;}
		}
		/// <summary>
		/// 小组成员编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 0普通学生，1.学生组长;2 小组指导老师
		/// </summary>
		public int UserRole
		{
			set{ _userrole=value;}
			get{return _userrole;}
		}
		#endregion Model

	}
}

