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
        /// <summary>
        /// 列表总数
        /// </summary>
        public int rowscount { get; set; }

        #endregion

        #region Model
        private int _HelpID;
        private string _Title;
        private string _Content;
        private int _SysID;
        private int _OrganizationID;
        private string _UpdateTime;
        private int _Clicks;

        /// <summary>
        /// 使用指南编号
        /// </summary>
        public int HelpID
        {
            set { _HelpID = value; }
            get { return _HelpID; }
        }

        /// <summary>
        /// 使用指南标题
        /// </summary>
        public string Title
        {
            set { _Title = value; }
            get { return _Title; }
        }

        /// <summary>
        /// 使用指南内容
        /// </summary>
        public string Content
        {
            set { _Content = value; }
            get { return _Content; }
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
        /// 更新时间
        /// </summary>
        public string UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
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
