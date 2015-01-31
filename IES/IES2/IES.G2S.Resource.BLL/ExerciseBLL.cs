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
        public ExerciseBLL()
        {

        }

        #region  列表

        /// <summary>
        /// 习题列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<Exercise> Exercise_Search(Exercise model, Key key, int PageSize, int PageIndex)
        {
            return ExerciseDAL.Exercise_Search(model, key, PageSize, PageIndex);
        }

        #endregion 

        #region 详细信息


        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public IES.Resource.Model.IExercise Exercise_Get(IExercise model)
        {
            Exercise o = model as Exercise;

            return ExerciseDAL.Exercise_Get(model);
        }

        public ExerciseInfo ExerciseInfo_Get(ExerciseInfo model)
        {
            return ExerciseDAL.ExerciseInfo_Get(model);
        }

        public ExerciseInfo ExerciseInfo_GetListen(ExerciseInfo model)
        {
            return ExerciseDAL.ExerciseInfo_GetListen(model);
        }
        #endregion 

        #region 新增

        public bool Exercise_ADD(IExercise model)
        {

            if (model is ExerciseAnswercardInfo)
            {

            }

            if (model is ExerciseCompInfo)
            {

            }

            if (model is ExerciseCommon)
            {
                ExerciseDAL.ExerciseCommon_ADD(model as ExerciseCommon);
            }

            if (model is ExerciseInfo)
            {
                ExerciseDAL.ExerciseInfo_ADD(model as ExerciseInfo);
            }

            return true;
        }


        #endregion 

        #region 对象更新

        public bool Exercise_Upd(IExercise  model)
        {
            if (model is ExerciseAnswercardInfo)
            {

            }

            if (model is ExerciseCompInfo)
            {

            }

            if (model is ExerciseCommon)
            {

            }

            if (model is ExerciseInfo)
            {
                ExerciseDAL.ExerciseInfo_Upd(model as ExerciseInfo);
            }

            return true;
        }

        #endregion 

        #region 单个属性更新

        public bool Exercise_Diffcult_Upd(Exercise model)
        {
            return ExerciseDAL.Exercise_Diffcult_Upd(model);
        }

        public bool Exercise_Scope_Upd(Exercise model)
        {
            return ExerciseDAL.Exercise_Scope_Upd(model);
        }

        public bool Exercise_ShareRange_Upd(Exercise model)
        {
            return ExerciseDAL.Exercise_ShareRange_Upd(model);
        }


        #endregion 

        #region 属性批量操作

        public bool Exercise_Batch_Diffcult(List<IExercise> list, int diffcult)
        {

            return ExerciseDAL.Exercise_Batch_Diffcult(list, diffcult);

        }

        public bool Exercise_Batch_Del(List<IExercise> list)
        {
            return ExerciseDAL.Exercise_Batch_Del(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public bool Exercise_Batch_Scope(List<IExercise> list, int scope)
        {
            return ExerciseDAL.Exercise_Batch_Scope(list, scope);
        }

        /// <summary>
        /// 习题批量设置共享范围
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sharerange"></param>
        /// <returns></returns>
        public bool Exercise_Batch_ShareRange(List<IExercise> list, int sharerange)
        {
            return ExerciseDAL.Exercise_Batch_ShareRange(list, sharerange);
        }


        #endregion 

        #region 删除

        [PermissionsCallHandler(Order = 2)]
        [ExceptionCallHandler(Order = 1)]
        public bool Exercise_Del(IExercise mode)
        {

            return ExerciseDAL.Exercise_Del( mode );
        }

        #endregion

    }
}
