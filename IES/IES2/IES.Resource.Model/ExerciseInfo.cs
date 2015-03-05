using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    /// <summary>
    /// 获取普通习题的详细信息
    /// </summary>
    public class ExerciseInfo : IExercise 
    {
        public int ExerciseID { get; set; }
        public int ExerciseType { get; set; }
        /// <summary>
        /// 习题的所有的基本信息
        /// </summary>
        public ExerciseCommon exercisecommon { get; set; }

        /// <summary>
        /// 习题选项
        /// </summary>
        public List<ExerciseChoice> exercisechoicelist { get; set; }

        //单选
        public List<ExerciseInfo> Children { get; set; }

        public List<ExerciseInfo> ChildrenMultiple { get; set; }
        public List<ExerciseInfo> ChildrenFillBlank { get; set; }
    }
}
