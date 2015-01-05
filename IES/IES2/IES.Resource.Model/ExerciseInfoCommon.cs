using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 习题的公共信息
    /// </summary>
    public  class ExerciseInfoCommon
    {
        /// <summary>
        /// 习题的基本信息
        /// </summary>
        public Exercise exercise { get; set; }

        /// <summary>
        /// 附件信息
        /// </summary>
        public List<Attachment> attachmentlist { get; set; }

        /// <summary>
        /// 习题选项
        /// </summary>
        public List<ExerciseChoice> exercisechoicelist { get; set; }

        /// <summary>
        /// 习题关联知识点
        /// </summary>
        public List<Ken> kenlist { get; set; }


        /// <summary>
        /// 习题关联的关键字
        /// </summary>
        public List<Key> keylist { get; set; }

    }
}
