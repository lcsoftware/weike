using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{

    public class ExerciseAnswer
    {
        public ExerciseAnswer()
        {
            this.IsShow = 0;
        }
        public int UserID { get; set; }
        public int CheckUserID { get; set; }

        public int ExerciseID { get; set; }
        public string Conten { get; set; }

        public int Score { get; set; }

        public string Comment { get; set; }

        public int ExerciseType { get; set; }

        public float PaperExerciseScore { get; set; }
        //答题卡选项数
        public int ChoiceNum { get; set; }

        //是否展示
        public int IsShow { get; set; }
    }
}
