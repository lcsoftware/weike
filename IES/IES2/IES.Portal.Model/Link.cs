using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Portal.Model
{
    [Serializable]
    public partial class Link
    {
        public Link()
        {}
        #region  补充信息

        public int rowscount { get; set; }

        #endregion 

        #region Model
        private int _linkid;
        private string _title;
        private string _url;
        private int _sysid;
        private int _moduleid;
        private int _organizationid;
        private bool _isimg;
        private int _attachmentid;
        private int _clicks; 
        /// <summary>
        /// 友情链接ID
        /// </summary>
        public int Linkid
        {
            get { return _linkid; }
            set { _linkid = value; }
        }
        /// <summary>
        /// 友情链接标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
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
        /// 功能模块ID
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
        /// <summary>
        /// 是否图片链接
        /// </summary>
        public bool Isimg
        {
            get { return _isimg; }
            set { _isimg = value; }
        }
        /// <summary>
        /// 附件ID
        /// </summary>
        public int Attachmentid
        {
            get { return _attachmentid; }
            set { _attachmentid = value; }
        }
        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            get { return _clicks; }
            set { _clicks = value; }
        }
        #endregion
    }
}
