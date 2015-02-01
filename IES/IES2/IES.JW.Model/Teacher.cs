using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    /// <summary>
    /// 站内用户教师信息
    /// </summary>
    [Serializable]
    public partial class Teacher
    {
        #region 补充信息
        public string Key { get; set; }
        #endregion
        public Teacher() { }
        #region Model

        public int RowNum { get; set; }
        public int UserID { get; set; }
        public string UserNo { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string OrganizationName { get; set; }

        public int rowscount { get; set; }
        #endregion


    }
}
