/**  版本信息模板在安装目录下，可自行修改。
* CfgSchool.cs
*
* 功 能： N/A
* 类 名： CfgSchool
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.JW.Model
{
    /// <summary>
    /// 系统底部版权\联系方式等信息

    /// </summary>
    [Serializable]
    public partial class CfgSchool
    {
        public CfgSchool()
        { }

        #region  补充信息
        public int UserType { get; set; }

        public string UserName { get; set; }

        #endregion

        #region Model
        private int _cfgschoolid;
        private int _organizationid = 0;
        private int _sysid = 1;
        private int _moduleid = 0;
        private string _schoolname;
        private string _schoolnameen;
        private string _orgname;
        private string _orgnameen;
        private string _icp;
        private string _schooltel;
        private string _companytel;
        private string _schoolemail;
        private string _companyemail;
        private string _schoolqq;
        private string _companyqq;
        private int _teacherspace;
        private int _studentspace;
        /// <summary>
        /// 
        /// </summary>
        public int CfgSchoolID
        {
            set { _cfgschoolid = value; }
            get { return _cfgschoolid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrganizationID
        {
            set { _organizationid = value; }
            get { return _organizationid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SysID
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SchoolNameEn
        {
            set { _schoolnameen = value; }
            get { return _schoolnameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrgName
        {
            set { _orgname = value; }
            get { return _orgname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrgNameEn
        {
            set { _orgnameen = value; }
            get { return _orgnameen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ICP
        {
            set { _icp = value; }
            get { return _icp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SchoolTel
        {
            set { _schooltel = value; }
            get { return _schooltel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyTel
        {
            set { _companytel = value; }
            get { return _companytel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SchoolEmail
        {
            set { _schoolemail = value; }
            get { return _schoolemail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyEmail
        {
            set { _companyemail = value; }
            get { return _companyemail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SchoolQQ
        {
            set { _schoolqq = value; }
            get { return _schoolqq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyQQ
        {
            set { _companyqq = value; }
            get { return _companyqq; }
        }

        public string LOGO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TeacherSpace
        {
            set { _teacherspace = value; }
            get { return _teacherspace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int StudentSpace
        {
            set { _studentspace = value; }
            get { return _studentspace; }
        }
        #endregion Model

    }
}

