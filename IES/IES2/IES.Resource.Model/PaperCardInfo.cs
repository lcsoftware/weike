using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 答题卡试卷的详细信息
    /// </summary>
    public class PaperCardInfo : IPaper  
    {

        public int PaperID { get; set; }

        public int Type { get; set; }
        /// <summary>
        /// 试卷的基本信息
        /// </summary>
        public Paper paper { get; set; }

        /// <summary>
        /// 答题卡中的习题
        /// </summary>
        public List<PaperCardexercise> papercardexerciselist { get; set; }
    }
}
