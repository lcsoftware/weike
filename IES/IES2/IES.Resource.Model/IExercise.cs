using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    public  interface IExercise
    {
        int  ExerciseID { get; set; }
        int ExerciseType { get; set; }
    }
}
