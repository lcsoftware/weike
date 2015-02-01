using System;

namespace IES.JW.Model
{
    [Serializable]
    public partial class OCDefaultColumn
    {
        public OCDefaultColumn()
        { }
        #region 补充信息
        public int topbm { get; set; }
        #endregion
        #region Model
        private int _ColumID;
        private string _Name;
        private int _ParentID;
        private int _Orde;

        /// <summary>
        /// 
        /// </summary>
        public int ColumID
        {
            set { _ColumID = value; }
            get { return _ColumID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ParentID
        {
            set { _ParentID = value; }
            get { return _ParentID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Orde
        {
            set { _Orde = value; }
            get { return _Orde; }
        }

        #endregion

    }
}
