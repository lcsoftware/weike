using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Portal.Model
{
    [Serializable]
    public partial class News
    {
        public News()
        { }
        #region 补充信息
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime _startdate;

        public DateTime Startdate
        {
            get { return _startdate; }
            set { _startdate = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime _enddatef;

        public DateTime Enddatef
        {
            get { return _enddatef; }
            set { _enddatef = value; }
        }

        public int rowscount { get; set; }

        #endregion
        #region Model
        private int _newsid;
        private string _title;
        private string _content;
        private int _sectionid;
        private int _sectionchild;
        private DateTime _createdate;
        private DateTime _enddate;
        private int _clicks;
        private bool _isimportant;
        private bool _istop;
        private int _sysid;
        private int _moduleid;
        private int _organizationid; 
        /// <summary>
        /// 新闻公告ID
        /// </summary>
        public int Newsid
        {
            get { return _newsid; }
            set { _newsid = value; }
        }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 新闻公告对应的版块编号
        /// </summary>
        public int Sectionid
        {
            get { return _sectionid; }
            set { _sectionid = value; }
        }
        /// <summary>
        /// 子版块
        /// </summary>
        public int Sectionchild
        {
            get { return _sectionchild; }
            set { _sectionchild = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime Enddate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            get { return _clicks; }
            set { _clicks = value; }
        }
        /// <summary>
        /// 是否是重要新闻
        /// </summary>
        public bool Isimportant
        {
            get { return _isimportant; }
            set { _isimportant = value; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool Istop
        {
            get { return _istop; }
            set { _istop = value; }
        }
        /// <summary>
        /// 系统ID
        /// </summary>
        public int Sysid
        {
            get { return _sysid; }
            set { _sysid = value; }
        }
        /// <summary>
        /// 模块ID
        /// </summary>
        public int Moduleid
        {
            get { return _moduleid; }
            set { _moduleid = value; }
        }
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public int Organizationid
        {
            get { return _organizationid; }
            set { _organizationid = value; }
        }
        #endregion
    }
}
