using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    [Serializable]
    public partial class NoticeResponse
    {
        #region  补充信息
        public DateTime CurrentTime { get; set; }
        public int rowscount { get; set; }
        #endregion

        public NoticeResponse()
        { }

        public int ResponseID { get; set; }
        public int NoticeID { get; set; }
        public string Conten { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string ClassName { get; set; }       
        public DateTime UpdateTime { get; set; }
        public int IsTop { get; set; }
    }
}
