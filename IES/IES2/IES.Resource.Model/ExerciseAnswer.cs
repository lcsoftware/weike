using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
   public class ExerciseAnswer
    {
       public int UserID { get; set; }
       public int CheckUserID { get; set; }

       public int ExerciseID { get; set; }
       public string Conten { get; set; }

       public int Score { get; set; }

       public string Comment { get; set; }
    }
}
