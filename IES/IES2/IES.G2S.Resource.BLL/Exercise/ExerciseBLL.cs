using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.Resource.DAL;
using IES.Resource.Model ;
using IES.AOP.G2S;
using IES.G2S.Resource.IBLL;

namespace IES.G2S.Resource.BLL
{
    public class ExerciseBLL : IExerciseBLL
    {
        public  ExerciseBLL()
        {

        }

        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public IES.Resource.Model.Exercise  Exercise_Get(int ID)
        {
            return ExerciseDAL.Exercise_Get(ID);
        }


        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public void Exercise_Del(int id)
        {
            ExerciseDAL.Exercise_Del(id);
        }

    }
}
