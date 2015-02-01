using System;

namespace IES.Portal.Model
{
    [Serializable]
    public partial class NewsSection
    {
        public NewsSection()
        {}
        #region Model
        private int _SectionID;
        private string _SectionName;
        private int _Orde;
        private bool _IsDeleted;

        /// <summary>
        /// 新闻公告所属板块编号
        /// </summary>
        public int SectionID
        {
            set{ _SectionID = value; }
            get{ return _SectionID; }
        }

        /// <summary>
        /// 新闻公告所属板块名称
        /// </summary>
        public string SectionName
        {
            set{ _SectionName = value; }
            get{ return _SectionName; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Orde
        {
            set{ _Orde = value; }
            get{ return _Orde; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted
        {
            set{ _IsDeleted = value; }
            get{ return _IsDeleted; }
        }

        #endregion
    }
}
