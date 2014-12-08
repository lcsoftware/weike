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
    public class ExerciseCommon : IExercise 
    {
        public int ExerciseID { get; set; }
        public int ExerciseType { get; set; }

        /// <summary>
        /// 习题的概信息
        /// </summary>
        public Exercise exercise { get; set; }

        /// <summary>
        /// 附件信息
        /// </summary>
        public List<Attachment> attachmentlist { get; set; }

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
