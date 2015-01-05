using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public class OCClassRegStudent
    {
        public int UserID { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string SpecialtyName { get; set; }
        public string ClassName { get; set; }
        public DateTime RegDate { get ; set; }
        public int TeachingClassID { get; set; }
        /// <summary>
        /// 国际英语1班（39人）陈晨
        /// </summary>
        public string  TeachingClassName { get; set; }
    }
}
