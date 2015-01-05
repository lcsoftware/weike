/**  版本信息模板在安装目录下，可自行修改。

* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:47   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐

*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│

*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│

*└──────────────────────────────────┘

*/
using System;
namespace IES.SYS.Model
{
	/// <summary>
	/// 用户表

	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}

        #region 补充信息

        /// <summary>
        /// 用户图片信息
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 教学组织名
        /// </summary>
        public string organizationname { get; set; }
        /// <summary>
        /// 专业名
        /// </summary>
        public string specialtyname { get; set; }
        /// <summary>
        /// 班级名
        /// </summary>
        public string classname { get; set; }

        #endregion 

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
		private int _organizationid=0;
		private string _ranks;
		private int? _entrydate;
		private int _specialtyid=0;
		private int _classid=0;
        private int _disksize;
		private bool _islocked= false;
		private int _usertype=2;
        private bool _isregister=false;
        private string _brief;
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
		/// 用户编号
		/// </summary>
		public string UserNo
		{
			set{ _userno=value;}
			get{return _userno;}
		}
		/// <summary>
		/// 姓名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 英文名

		/// </summary>
		public string UserNameEn
		{
			set{ _usernameen=value;}
			get{return _usernameen;}
		}
		/// <summary>
		/// 登录名

		/// </summary>
		public string LoginName
		{
			set{ _loginname=value;}
			get{return _loginname;}
		}
		/// <summary>
		/// 密码
		/// </summary>
		public string Pwd
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 昵称
		/// </summary>
		public string Nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 邮件地址
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}

        public string Tel { get; set; }


		/// <summary>
		/// 移动电话
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
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }

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
        /// 分配的磁盘配额

        /// </summary>
        public int DiskSize
        {
            set { _disksize = value; }
            get { return _disksize; }
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
		/// 用户类别： 0:超级管理员，1:子管理员，2:学生，3:教师，4:系统外用户

		/// </summary>
		public int UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
        /// <summary>
        /// 登录状态

        /// </summary>
        public bool IsRegister
        {
            set { _isregister = value; }
            get { return _isregister; }
        }
        /// <summary>
        /// 简介

        /// </summary>
        public string Brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
		/// <summary>
		/// 删除状态

		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

