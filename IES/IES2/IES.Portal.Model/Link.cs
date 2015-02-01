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
        /// <summary>
        /// 列表总数
        /// </summary>
        public int rowscount { get; set; }

        #endregion 

        #region Model
        private int _LinkID;
        private string _Title;
        private string _URL;
        private int _SysID;
        private int _OrganizationID;
        private bool _IsIMG;
        private int _Clicks;

        /// <summary>
        /// 友情链接编号
        /// </summary>
        public int LinkID
        {
            set { _LinkID = value; }
            get { return _LinkID; }
        }

        /// <summary>
        /// 友情链接标题
        /// </summary>
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }

        /// <summary>
        /// 友情链接地址
        /// </summary>
        public string URL
        {
            set { _URL = value; }
            get { return _URL; }
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

        /// <summary>
        /// 是否是图片链接
        /// </summary>
        public bool IsIMG
        {
            set { _IsIMG = value; }
            get { return _IsIMG; }
        }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Clicks
        {
            set { _Clicks = value; }
            get { return _Clicks; }
        }

        #endregion

    }
}
