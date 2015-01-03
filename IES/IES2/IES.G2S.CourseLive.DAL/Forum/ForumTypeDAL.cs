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
        /// 习题查询
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key">关键字</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<ForumType> ForumType_List(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
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
                    p.Add("@ForumTypeID" , model.ForumTypeID );
                    var multi = conn.QueryMultiple("ForumTypeInfo_Get", p , commandType: CommandType.StoredProcedure);
                    var forumtype = multi.Read<ForumType>().Single();
                    var forumclasslist = multi.Read<ForumClass>().ToList();
                    ft.forumtype = forumtype ;
                    ft.forumclasslist = forumclasslist ;
                    return ft ;
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
        public static bool ForumType_ADD(ForumTypeInfo model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ForumTypeID", dbType: DbType.Int32, direction: ParameterDirection.Output  );
                    p.Add("@OCID", model.forumtype.OCID );
                    p.Add("@CourseID", model.forumtype.CourseID);
                    p.Add("@Title", model.forumtype.Title);
                    p.Add("@IsEssence", model.forumtype.IsIsEssence);
                    p.Add("@Brief", model.forumtype.Brief);
                    p.Add("@IsPublic", model.forumtype.IsPublic);
                    p.Add("@UserID", model.forumtype.UserID);
                    conn.Execute("ForumType_ADD", p, commandType: CommandType.StoredProcedure);
                    model.forumtype.ForumTypeID = p.Get<int>("ForumTypeID");

                    foreach (var o in model.forumclasslist)
                    {
                        var p1 = new DynamicParameters();
                        //TODO:

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


        public static bool ForumType_Upd(ForumType model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p1 = new DynamicParameters();
                    p1.Add("@ForumTypeID", model.ForumTypeID );
                    p1.Add("@Title", model.Title );
                    p1.Add("@IsEssence", model.IsIsEssence);
                    p1.Add("@Brief", model.Brief);
                    p1.Add("@IsPublic", model.IsPublic);
                    conn.Execute("ForumType_Upd", p1, commandType: CommandType.StoredProcedure);

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
        public static bool ForumType_Del(ForumTopicType model)
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
