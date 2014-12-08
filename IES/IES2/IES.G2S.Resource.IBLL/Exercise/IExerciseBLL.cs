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


            IES.Resource.Model.Exercise Exercise_Get(int ID);

            void Exercise_Del(int id);
        }

}
