using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 答题卡习题的详细信息
    /// </summary>
    public class ExerciseAnswercardInfo : IExercise 
    {
        public int ExerciseID { get; set; }
        public int ExerciseType { get; set; }

        /// <summary>
        /// 习题的基本信息类
        /// </summary>
        public ExerciseCommon exercisecommon { get; set; }

        /// <summary>
        /// 答题卡习题列表
        /// </summary>
        public List<ExerciseAnswercard> exerciseanswercardlist { get; set; }


    }
}
