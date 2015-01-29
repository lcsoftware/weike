using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.Resource.Model;

namespace IES.G2S.Resource.DAL
{
    public class ExerciseDAL
    {
        #region 列表
        /// <summary>
        /// 习题查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key">关键字</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<Exercise> Exercise_Search( Exercise model , Key key , int PageSize ,int PageIndex )
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Searchkey", model.Conten);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@UserID", model.CreateUserID);
                    p.Add("@ExerciseType", model.ExerciseType);
                    p.Add("@Diffcult", model.Diffcult);
                    p.Add("@Scope", model.Scope);
                    p.Add("@ShareRange", model.ShareRange);
                    p.Add("@Key", key.KeyID);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<Exercise>("Exercise_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Exercise>();
            }

        }

        #endregion 

        #region 详细信息
        public static IExercise Exercise_Get(IExercise model)
        {
            return new ExerciseCommon();
        }


        /// <summary>
        /// 返回习题的完全信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ExerciseInfo ExerciseInfo_Get(IExercise model)
        {
      
            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    ExerciseInfo ef = new ExerciseInfo() {
                        exercisechoicelist = new List<ExerciseChoice>(),
                        exercisecommon = new ExerciseCommon()
                        {
                            kenlist = new List<Ken>(),
                            keylist = new List<Key>(),
                            exercise = new IES.Resource.Model.Exercise(),
                            attachmentlist = new List<Attachment>()
                        } 
                    };
                    var p = new DynamicParameters();
                    p.Add("@ExerciseID", model.ExerciseID);

                    var multi = conn.QueryMultiple("ExerciseInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var exercise = multi.Read<Exercise>().Single();
                    var attachmentlist = multi.Read<Attachment>().ToList();
                    var keylist = multi.Read<Key>().ToList();
                    var kenlist = multi.Read<Ken>().ToList();

                    var exercisechoicelist = multi.Read<ExerciseChoice>().ToList();
                    ef.exercisechoicelist = exercisechoicelist;

                    ef.exercisecommon.exercise = exercise;
                    ef.exercisecommon.attachmentlist = attachmentlist;
                    ef.exercisecommon.kenlist = kenlist;
                    ef.exercisecommon.keylist = keylist;

                    return ef;
                }
            }
            catch (Exception e)
            {
                return null  ;
            }

        }

        #endregion 

        #region 新增



        /// <summary>
        /// 添加习题的通用信息，问答题
        /// </summary>
        /// <param name="model"></param>
        public static bool ExerciseCommon_ADD( ExerciseCommon model )
        {
            try 
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();

                    p.Add("@ExerciseID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.exercise.OCID);
                    p.Add("@CourseID", model.exercise.CourseID);
                    p.Add("@OwnerUserID", model.exercise.OwnerUserID);
                    p.Add("@CreateUserID", model.exercise.CreateUserID);
                    p.Add("@ParentID", model.exercise.ParentID);
                    p.Add("@ExerciseType", model.exercise.ExerciseType);
                    p.Add("@Diffcult", model.exercise.Diffcult);
                    p.Add("@Scope", model.exercise.Scope);
                    p.Add("@ShareRange", model.exercise.ShareRange);
                    p.Add("@Brief", model.exercise.Brief);
                    p.Add("@Conten", model.exercise.Conten);
                    p.Add("@Answer", model.exercise.Answer);
                    p.Add("@Analysis", model.exercise.Analysis);
                    p.Add("@ScorePoint", model.exercise.ScorePoint);
                    p.Add("@Score", model.exercise.Score);
                    p.Add("@IsRand", model.exercise.IsRand);
                    conn.Execute("Exercise_ADD", p, commandType: CommandType.StoredProcedure);
                    model.exercise.ExerciseID = p.Get<int>("ExerciseID");

                    if (model.kenlist != null)
                    {
                        foreach (var ken in model.kenlist)
                        {
                            var p1 = new DynamicParameters();
                            p1.Add("@ExerciseID", model.exercise.ExerciseID);
                            p1.Add("@KenID", ken.KenID);
                            p1.Add("@OCID", model.exercise.OCID);
                            p1.Add("@CourseID", model.exercise.CourseID);
                            p1.Add("@OwnerUserID", model.exercise.OwnerUserID);
                            p1.Add("@CreateUserID", model.exercise.CreateUserID);
                            p1.Add("@Name", ken.Name);
                            conn.Execute("Exercise_Ken_Edit", p1, commandType: CommandType.StoredProcedure);
                        }
                    }

                    if (model.keylist != null)
                    {
                        foreach (var key in model.keylist)
                        {
                            var p1 = new DynamicParameters();
                            p1.Add("@ExerciseID", model.exercise.ExerciseID);
                            p1.Add("@KeyID", key.KeyID);
                            p1.Add("@OCID", model.exercise.OCID);
                            p1.Add("@CourseID", model.exercise.CourseID);
                            p1.Add("@OwnerUserID", model.exercise.OwnerUserID);
                            p1.Add("@CreateUserID", model.exercise.CreateUserID);
                            p1.Add("@Name", key.Name);
                            conn.Execute("Exercise_Key_Edit", p1, commandType: CommandType.StoredProcedure);
                        }
                    }

                    if (model.attachmentlist != null)
                    {
                        foreach (var attach in model.attachmentlist)
                        {
                            var p1 = new DynamicParameters();
                            p1.Add("@ExerciseID", model.exercise.ExerciseID);
                            p1.Add("@Guid", attach.Guid);
                            conn.Execute("Exercise_Attachment_ADD", p1, commandType: CommandType.StoredProcedure);
                        }
                    }

                    return true;
                }   
            }
            catch (Exception e)
            {
                return false ;
            }
        }


        /// <summary>
        /// 选择题添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseInfo_ADD( ExerciseInfo model )
        {
            try
            {
                ExerciseCommon_ADD( model.exercisecommon );
                ExerciseChoice_ADD( model );
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 答题卡习题新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseAnswercardInfo_ADD(ExerciseAnswercardInfo model)
        {
            try
            {
                ExerciseCommon_ADD(model.exercisecommon);
                ExerciseAnswercard_ADD(model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 选择题的选项新增
        /// </summary>
        /// <param name="exercisechoicelist"></param>
        /// <returns></returns>
        public static bool ExerciseChoice_ADD(  ExerciseInfo model  )
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    List<ExerciseChoice> exercisechoicelist = model.exercisechoicelist;
                    foreach (var choice in exercisechoicelist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercisecommon.exercise.ExerciseID );
                        p1.Add("@Conten", choice.Conten);
                        p1.Add("@IsCorrect", choice.IsCorrect);
                        p1.Add("@Grou", choice.Grou);
                        p1.Add("@OrderNum", choice.OrderNum);
                        conn.Execute("ExerciseChoice_ADD", p1, commandType: CommandType.StoredProcedure);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 答题卡内容添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseAnswercard_ADD( ExerciseAnswercardInfo  model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    List<ExerciseAnswercard> exerciseanswercardlist = model.exerciseanswercardlist ;
                    foreach (var answercard in exerciseanswercardlist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercisecommon.exercise.ExerciseID);
                        p1.Add("@CorrectAnswer", answercard.CorrectAnswer );
                        p1.Add("@Score", answercard.Score );
                        p1.Add("@Type", answercard.Type );
                        p1.Add("@ChoiceNum", answercard.ChoiceNum );
                        conn.Execute("ExerciseAnswercard_ADD", p1, commandType: CommandType.StoredProcedure);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        #endregion 

        #region 对象更新

        public static bool ExerciseCommon_Upd(ExerciseCommon model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();

                    p.Add("@ExerciseID", model.exercise.ExerciseID);
                    p.Add("@ExerciseType", model.exercise.ExerciseType);
                    p.Add("@Diffcult", model.exercise.Diffcult);
                    p.Add("@Scope", model.exercise.Scope);
                    p.Add("@ShareRange", model.exercise.ShareRange);
                    p.Add("@Brief", model.exercise.Brief);
                    p.Add("@Conten", model.exercise.Conten);
                    p.Add("@Answer", model.exercise.Answer);
                    p.Add("@Analysis", model.exercise.Analysis);
                    p.Add("@ScorePoint", model.exercise.ScorePoint);
                    p.Add("@Score", model.exercise.Score);
                    p.Add("@IsRand", model.exercise.IsRand);
                    conn.Execute("Exercise_Upd", p, commandType: CommandType.StoredProcedure);

                    foreach (var ken in model.kenlist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercise.ExerciseID);
                        p1.Add("@KenID", ken.KenID);
                        p1.Add("@OCID", model.exercise.OCID);
                        p1.Add("@CourseID", model.exercise.CourseID);
                        p1.Add("@OwnerUserID", model.exercise.OwnerUserID);
                        p1.Add("@CreateUserID", model.exercise.CreateUserID);
                        p1.Add("@Name", ken.Name);
                        conn.Execute("Exercise_Ken_Edit", p1, commandType: CommandType.StoredProcedure);
                    }

                    foreach (var key in model.keylist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercise.ExerciseID);
                        p1.Add("@KeyID", key.KeyID);
                        p1.Add("@OCID", model.exercise.OCID);
                        p1.Add("@CourseID", model.exercise.CourseID);
                        p1.Add("@OwnerUserID", model.exercise.OwnerUserID);
                        p1.Add("@CreateUserID", model.exercise.CreateUserID);
                        p1.Add("@Name", key.Name);
                        conn.Execute("Exercise_Key_Edit", p1, commandType: CommandType.StoredProcedure);
                    }

                    foreach (var attach in model.attachmentlist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercise.ExerciseID);
                        p1.Add("@Guid", attach.Guid);
                        conn.Execute("Exercise_Attachment_Edit", p1, commandType: CommandType.StoredProcedure);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 选择题更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseInfo_Upd(ExerciseInfo model)
        {
            try
            {
                ExerciseCommon_Upd(model.exercisecommon);
                ExerciseChoice_Upd(model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 答题卡习题更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseAnswercardInfo_Upd(ExerciseAnswercardInfo model)
        {
            try
            {
                ExerciseCommon_Upd(model.exercisecommon);
                ExerciseAnswercard_Upd(model);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 选择题的选项更新
        /// </summary>
        /// <param name="exercisechoicelist"></param>
        /// <returns></returns>
        public static bool ExerciseChoice_Upd(ExerciseInfo model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    List<ExerciseChoice> exercisechoicelist = model.exercisechoicelist;
                    foreach (var choice in exercisechoicelist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ChoiceID", choice.ChoiceID);
                        p1.Add("@Conten", choice.Conten);
                        p1.Add("@IsCorrect", choice.IsCorrect);
                        p1.Add("@Grou", choice.Grou);
                        p1.Add("@OrderNum", choice.OrderNum);
                        conn.Execute("ExerciseChoice_Upd", p1, commandType: CommandType.StoredProcedure);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 答题卡内容更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ExerciseAnswercard_Upd(ExerciseAnswercardInfo model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    List<ExerciseAnswercard> exerciseanswercardlist = model.exerciseanswercardlist;
                    foreach (var answercard in exerciseanswercardlist)
                    {
                        var p1 = new DynamicParameters();
                        p1.Add("@ExerciseID", model.exercisecommon.exercise.ExerciseID);
                        p1.Add("@CorrectAnswer", answercard.CorrectAnswer);
                        p1.Add("@Score", answercard.Score);
                        p1.Add("@Type", answercard.Type);
                        p1.Add("@ChoiceNum", answercard.ChoiceNum);
                        conn.Execute("ExerciseAnswercard_Upd", p1, commandType: CommandType.StoredProcedure);
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion 

        #region 单个属性更新

        public static bool Exercise_Diffcult_Upd(Exercise model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ExerciseID", model.ExerciseID);
                    conn.Execute("Exercise_Diffcult_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }


        public static bool Exercise_Scope_Upd(Exercise model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ExerciseID", model.ExerciseID);
                    conn.Execute("Exercise_Scope_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public static bool Exercise_ShareRange_Upd(Exercise model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ExerciseID", model.ExerciseID);
                    conn.Execute("Exercise_ShareRange_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }





        #endregion 

        #region 属性批量操作

        /// <summary>
        /// 习题难度系统批量操作
        /// </summary>
        /// <param name="list"></param>
        /// <param name="diffcult"></param>
        /// <returns></returns>
        public static bool Exercise_Batch_Diffcult( List<IExercise> list , int  diffcult  )
        {

            return true;
        }

        public static bool Exercise_Batch_Del(List<IExercise> list )
        {

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static bool Exercise_Batch_Scope(List<IExercise> list, int scope)
        {

            return true;
        }


        /// <summary>
        /// 习题批量设置共享范围
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sharerange"></param>
        /// <returns></returns>
        public static bool Exercise_Batch_ShareRange(List<IExercise> list, int sharerange)
        {

            return true;
        }


        #endregion 

        #region 删除
        /// <summary>
        /// 习题删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Exercise_Del(IExercise model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ExerciseID", model.ExerciseID);
                    conn.Execute("Exercise_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }

        }
        #endregion

    }
}
