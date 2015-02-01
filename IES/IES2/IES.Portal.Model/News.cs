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
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 所属板块名称
        /// </summary>
        public string SectionName { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 列表总数
        /// </summary>
        public int rowscount { get; set; }

        #endregion
        #region Model
        private int _NewsID;
        private string _Title;
        private string _Content;
        private int _SectionID;
        private DateTime _CreateDate;
        private DateTime _EndDate;
        private int _Clicks;
        private bool _IsImportant;
        private bool _IsTop;
        private int _SysID;
        private int _OrganizationID;

        /// <summary>
        /// 新闻公告编号
        /// </summary>
        public int NewsID
        {
            set { _NewsID = value; }
            get { return _NewsID; }
        }

        /// <summary>
        /// 新闻公告标
        /// </summary>
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }

        /// <summary>
        /// 新闻公告内容
        /// </summary>
        public string Content
        {
            set { _Content = value; }
            get { return _Content; }
        }

        /// <summary>
        /// 新闻公告所属板块编号
        /// </summary>
        public int SectionID
        {
            set { _SectionID = value; }
            get { return _SectionID; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _CreateDate = value; }
            get { return _CreateDate; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime EndDate
        {
            set { _EndDate = value; }
            get { return _EndDate; }
        }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            set { _Clicks = value; }
            get { return _Clicks; }
        }

        /// <summary>
        /// 是否重要新闻
        /// </summary>
        public bool IsImportant
        {
            set { _IsImportant = value; }
            get { return _IsImportant; }
        }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop
        {
            set { _IsTop = value; }
            get { return _IsTop; }
        }

        /// <summary>
        /// 子系统编号
        /// </summary>
        public int SysID
        {
            set { _SysID = value; }
            get { return _SysID; }
        }

        /// <summary>
        /// 组织机构编号
        /// </summary>
        public int OrganizationID
        {
            set { _OrganizationID = value; }
            get { return _OrganizationID; }
        }

        #endregion
       
    }
}
