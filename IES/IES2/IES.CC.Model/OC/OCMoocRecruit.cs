/**  版本信息模板在安装目录下，可自行修改。
* OCMoocRecruit.cs
*
* 功 能： N/A
* 类 名： OCMoocRecruit
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:27   N/A    初版
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
    /// MOOC招生
    /// </summary>
    [Serializable]
    public partial class OCMoocRecruit
    {
        public OCMoocRecruit()
        { }
        #region Model
        private int _recruitid;
        private int _ocid;
        private int _jointype = 2;
        private DateTime _recruitstartdate = DateTime.Now;
        private DateTime _recruitenddate = DateTime.Now;
        private int _userlimit = 0;
        private int? _regnum;
        private bool _regstatus = false;
        public DateTime _startdate;
        public DateTime _enddate;
        private bool _recruitstatus = false;
        public DateTime _createtime;
        /// <summary>
        /// 
        /// </summary>
        public int RecruitID
        {
            set { _recruitid = value; }
            get { return _recruitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 1 随时加入 ； 2 统一开课 
        /// </summary>
        public int JoinType
        {
            set { _jointype = value; }
            get { return _jointype; }
        }
        /// <summary>
        /// 招生开始日期

        /// </summary>
        public DateTime RecruitStartDate
        {
            set { _recruitstartdate = value; }
            get { return _recruitstartdate; }
        }
        /// <summary>
        /// 招生截止日期
        /// </summary>
        public DateTime RecruitEndDate
        {
            set { _recruitenddate = value; }
            get { return _recruitenddate; }
        }
        /// <summary>
        /// 用户限制,0表示不限制

        /// </summary>
        public int UserLimit
        {
            set { _userlimit = value; }
            get { return _userlimit; }
        }
        /// <summary>
        /// 注册码

        /// </summary>
        public int? RegNum
        {
            set { _regnum = value; }
            get { return _regnum; }
        }
        /// <summary>
        /// 是否启用注册码

        /// </summary>
        public bool RegStatus
        {
            set { _regstatus = value; }
            get { return _regstatus; }
        }


        /// <summary>
        /// 运行开始时间
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }

        /// <summary>
        /// 运行结束时间
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }

        /// <summary>
        /// 招生是否暂停
        /// </summary>//
        public bool RecruitStatus
        {
            set { _recruitstatus = value; }
            get { return _recruitstatus; }
        }

        /// <summary>
        /// 招生是否暂停
        /// </summary>//
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

        #endregion Model


    }
}

