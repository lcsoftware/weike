using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
        /// <summary>
        /// 习题库操作类，需要拦截的方法都需要在接口中定义
        /// </summary>
        public interface IExerciseBLL
        {

            List<Exercise> Exercise_Search(Exercise model, Key key, int PageSize, int PageIndex);

            IES.Resource.Model.IExercise Exercise_Get(IExercise model);

            bool Exercise_ADD(IExercise model);

            bool Exercise_Upd(IExercise model);

            bool Exercise_Del(IExercise model);




        }

}
