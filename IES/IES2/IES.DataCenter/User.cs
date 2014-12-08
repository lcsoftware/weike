/**  版本信息模板在安装目录下，可自行修改。
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/27 8:35:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.DataCenter.Model
{
	/// <summary>
	/// 用户表
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
		#region Model
		private int _userid;
		private string _userno;
		private string _username;
		private string _usernameen;
		private string _loginname;
		private string _pwd;
		private string _nickname;
		private string _email;
		private string _mobile;
		private int _organizationid;
		private string _ranks;
		private int? _entrydate;
		private int _specialtyid=0;
		private int _classid=0;
		private bool _islocked= false;
		private int _identity=2;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserNo
		{
			set{ _userno=value;}
			get{return _userno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserNameEn
		{
			set{ _usernameen=value;}
			get{return _usernameen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginName
		{
			set{ _loginname=value;}
			get{return _loginname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 学院编号
		/// </summary>
		public int OrganizationID
		{
			set{ _organizationid=value;}
			get{return _organizationid;}
		}
		/// <summary>
		/// 职称
		/// </summary>
		public string Ranks
		{
			set{ _ranks=value;}
			get{return _ranks;}
		}
		/// <summary>
		/// 入学日期
		/// </summary>
		public int? EntryDate
		{
			set{ _entrydate=value;}
			get{return _entrydate;}
		}
		/// <summary>
		/// 专业编号
		/// </summary>
		public int SpecialtyID
		{
			set{ _specialtyid=value;}
			get{return _specialtyid;}
		}
		/// <summary>
		/// 行政班编号
		/// </summary>
		public int ClassID
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 是否锁定
		/// </summary>
		public bool IsLocked
		{
			set{ _islocked=value;}
			get{return _islocked;}
		}
		/// <summary>
		/// 0:超级管理员，1:子管理员，2:学生，3:教师，4:系统外用户
		/// </summary>
		public int Identity
		{
			set{ _identity=value;}
			get{return _identity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

