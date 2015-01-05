using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Portal.Model
{
    [Serializable]
    public partial class Help
    {
        public Help()
        {}
        #region  补充信息

        public int rowscount { get; set; }

        #endregion
        #region Model
        private int _helpid;
        private string _title;
        private string _content;
        private int _sysid;
        private int _organizationid;
        private int _moduleid;
        private DateTime _updatetime;
        private int _clicks;  
        /// <summary>
        /// 使用指南ID
        /// </summary>
        public int Helpid
        {
            get { return _helpid; }
            set { _helpid = value; }
        }
        /// <summary>
        /// 使用指南标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
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
        /// 组织机构ID
        /// </summary>
        public int Organizationid
        {
            get { return _organizationid; }
            set { _organizationid = value; }
        }
        /// <summary>
        /// 功能模块ID
        /// </summary>
        public int Moduleid
        {
            get { return _moduleid; }
            set { _moduleid = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
        /// <summary>
        /// 访问量
        /// </summary>
        public int Clicks
        {
            get { return _clicks; }
            set { _clicks = value; }
        }
        #endregion
    }
}
