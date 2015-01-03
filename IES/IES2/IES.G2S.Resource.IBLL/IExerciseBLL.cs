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
            #region 列表

            List<Exercise> Exercise_Search(Exercise model, Key key, int PageSize, int PageIndex);


            #endregion 

            #region 详细信息

           IExercise Exercise_Get(IExercise model);


            #endregion

            #region  新增


           bool Exercise_ADD(IExercise model);


            #endregion

            #region 对象更新

           bool Exercise_Upd(IExercise model);

            #endregion

            #region 单个属性更新

            bool Exercise_Diffcult_Upd(Exercise model);

            bool Exercise_Scope_Upd(Exercise model);

            bool Exercise_ShareRange_Upd(Exercise model);


            #endregion

            #region 属性批量新增

            bool Exercise_Batch_Diffcult(List<IExercise> list, int diffcult);

            bool Exercise_Batch_Del(List<IExercise> list);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="list"></param>
            /// <param name="scope"></param>
            /// <returns></returns>
            bool Exercise_Batch_Scope(List<IExercise> list, int scope);


            /// <summary>
            /// 习题批量设置共享范围
            /// </summary>
            /// <param name="list"></param>
            /// <param name="sharerange"></param>
            /// <returns></returns>
            bool Exercise_Batch_ShareRange(List<IExercise> list, int sharerange);


            #endregion

            #region 删除

            bool Exercise_Del(IExercise model);


            #endregion


        }

}
