using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.CC.Forum.Model;
using IES.JW.Model;
using IES.DataBase;
using Dapper;


namespace IES.G2S.CourseLive.DAL.Forum
{

    /// <summary>
    /// 论坛版块
    /// </summary>
    public class ForumTypeDAL
    {
        #region  列表

        /// <summary>
        /// 论坛版块列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ForumType> ForumType_List(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();

                    p.Add("@OCID", model.OCID);
                    p.Add("@UserID", model.UserID);
                    return conn.Query<ForumType>("ForumType_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }




        #endregion

        #region 详细信息

        /// <summary>
        /// 获取论坛版块详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumTypeInfo ForumTypeInfo_Get(ForumType model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.CCService())
                {
                    ForumTypeInfo ft = new ForumTypeInfo();
                    var p = new DynamicParameters();
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    var multi = conn.QueryMultiple("ForumTypeInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var forumtype = multi.Read<ForumType>().Single();
                    var forumclasslist = multi.Read<ForumClass>().ToList();
                    ft.forumtype = forumtype;
                    ft.forumclasslist = forumclasslist;
                    return ft;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        #endregion

        #region  新增

        /// <summary>
        /// 论坛版块添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumType ForumType_ADD(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ForumTypeID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@Title", model.Title);
                    p.Add("@IsEssence", model.IsEssence);
                    p.Add("@Brief", model.Brief);
                    p.Add("@TeachingClassID", model.TeachingClassID);
                    p.Add("@IsPublic", model.IsPublic);
                    p.Add("@UserID", model.UserID);
                    conn.Execute("ForumType_ADD", p, commandType: CommandType.StoredProcedure);
                    model.ForumTypeID = p.Get<int>("ForumTypeID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        #endregion

        #region 对象更新


        public static bool ForumType_Upd(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    p.Add("@Title", model.Title);
                    p.Add("@IsEssence", model.IsEssence);
                    p.Add("@Brief", model.Brief);
                    p.Add("@IsPublic", model.IsPublic);
                    p.Add("@TeachingClassID", model.TeachingClassID);
                    conn.Execute("ForumType_Upd", p, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作





        #endregion

        #region 删除
        /// <summary>
        /// 论坛版块删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ForumType_Del(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ForumTypeID", model.ForumTypeID);
                    conn.Execute("ForumType_Del", p, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        #endregion
    }
}
