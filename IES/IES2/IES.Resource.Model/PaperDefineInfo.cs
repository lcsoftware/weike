using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{

    /// <summary>
    /// 试卷的定义的结构信息，分组、习题、策略等
    /// </summary>
    [Serializable]
    public class PaperDefineInfo :   IPaper   
    {

        public int PaperID { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 试卷基本信息
        /// </summary>
        public Paper paper { get; set; }

        /// <summary>
        /// 试卷的分组
        /// </summary>
        public List<PaperGroup> papergrouplist { get; set; }


        /// <summary>
        /// 分组下的附件
        /// </summary>
        public List<Attachment> attachmentlist { get; set; }


        /// <summary>
        /// 试卷分组的策略
        /// </summary>
        public List<PaperTactic> papertacticlist { get; set; }



        /// <summary>
        /// 分组下的习题，不同的题型
        /// </summary>
        public List<PaperExercise> exerciselist { get; set; }



    }
}
