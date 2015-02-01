using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    public class ExerciseType
    {
        public int ExerciseTypeID { get; set; }
        public string ExerciseTypeName { get; set; }
        /// <summary>
        /// 题型下的习题数量
        /// </summary>
        public int ExerciseCount { get; set; }
    }
}
