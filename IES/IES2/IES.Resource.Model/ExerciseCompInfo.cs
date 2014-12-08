using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 复合习题的详细信息
    /// </summary>
    public class ExerciseCompInfo : IExercise 
    {
        public int ExerciseID { get; set; }
        public int ExerciseType { get; set; }
        /// <summary>
        /// 习题的所有的基本信息
        /// </summary>
        public ExerciseCommon exercisecommon { get; set; }

        /// <summary>
        /// 复合题下所有的小题
        /// </summary>
        public List<Exercise> exerciselist { get; set; }


        /// <summary>
        /// 复合题下所有小题的附件
        /// </summary>
        public List<Attachment> attachmentlist { get; set; }


        /// <summary>
        /// 复合题下所有小题的选项
        /// </summary>
        public List<ExerciseChoice> exercisechoicelist { get; set; }


    }
}
