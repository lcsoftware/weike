using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
   public  class ExerciseAnswerInfo
    {
       public ExerciseAnswerInfo() {
           this.ExerciseAnswers = new List<ExerciseAnswer>(); 
       }

       public int UseTime { get; set; }

       public List<ExerciseAnswer> ExerciseAnswers { get; set; }

    }
}
