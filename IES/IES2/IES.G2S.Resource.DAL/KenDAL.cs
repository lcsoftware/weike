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
    public class KenDAL
    {

        #region  列表

        /// <summary>
        /// 获取知识点列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Ken> Ken_List(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);

                    return conn.Query<Ken>("Ken_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取知识点相关章节列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Chapter> Chapter_KenID_List(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@OCID", model.OCID);
                    return conn.Query<Chapter>("Chapter_KenID_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 章节关联的文件列表 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<File> File_KenID_ChapterID_List(Chapter model, Ken ken)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@UserID", model.CreateUserID);
                    p.Add("@OCID", ken.OCID);
                    p.Add("@KenID", ken.KenID);
                    return conn.Query<File>("File_KenID_ChapterID_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static IList<Exercise> Exercise_KenID_ChapterID_List(Chapter chapter, Ken ken)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", chapter.ChapterID);
                    p.Add("@UserID", ken.CreateUserID);
                    p.Add("@OCID", ken.OCID);
                    p.Add("@KenID", ken.KenID);
                    return conn.Query<Exercise>("Exercise_KenID_ChapterID_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static IList<Ken> Ken_FileFilter_ChapterID_List(Chapter chapter)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", chapter.ChapterID);
                    p.Add("@UserID", chapter.CreateUserID);
                    p.Add("@OCID", chapter.OCID);
                    return conn.Query<Ken>("Ken_FileFilter_ChapterID_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static IList<Ken> Ken_ExerciseFilter_ChapterID_List(Chapter chapter)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", chapter.ChapterID);
                    p.Add("@UserID", chapter.CreateUserID);
                    p.Add("@OCID", chapter.OCID);
                    return conn.Query<Ken>("Ken_ExerciseFilter_ChapterID_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取文件、习题相关有效知识点 
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Source"></param>
        /// <param name="UserID"></param>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public static List<Ken> ExerciseOrFile_Ken_List(string SearchKey,string Source,int UserID,int TopNum,int OCID)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@SearchKey", SearchKey);
                    p.Add("@Source", Source);
                    p.Add("@UserID", UserID);
                    p.Add("@TopNum", TopNum);

                    return conn.Query<Ken>("Resource_Ken_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Ken>();
            }
        }

        /// <summary>
        /// 获取知识点的关联文件列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<File> Ken_File_List(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@UserID", model.CreateUserID );
                    return conn.Query<File>("Ken_File_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public static List<Ken> Ken_ExerciseCount_List(int OCID, int UserID, int ExerciseType, int Diffcult)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@UserID", UserID);
                    p.Add("@ExerciseType", ExerciseType);
                    p.Add("@Diffcult", Diffcult);

                    return conn.Query<Ken>("Ken_ExerciseCount_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Ken>();
            }
        }



        #endregion

        #region 详细信息



        #endregion

        #region  新增

        /// <summary>
        /// 知识点新增

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Ken Ken_ADD(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CreateUserID", model.CreateUserID);
                    p.Add("@OwnerUserID", model.OwnerUserID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@OCID", model.OCID);
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@Name", model.Name);
                    p.Add("@Pingyin", model.Pingyin);
                    p.Add("@Requirement", model.Requirement);
                    conn.Execute("Ken_ADD", p, commandType: CommandType.StoredProcedure);
                    model.KenID = p.Get<int>("KenID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return model;
            }

        }


        #endregion

        #region 对象更新

        /// <summary>
        /// 知识点更新

        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Ken_Upd(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@UserID", model.CreateUserID);
                    p.Add("@OCID", model.OCID);
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@Name", model.Name);
                    p.Add("@Pingyin", model.Pingyin);
                    p.Add("@Requirement", model.Requirement);
                    conn.Execute("Ken_Upd", p, commandType: CommandType.StoredProcedure);
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
        ///  知识点删除

        /// </summary>
        /// <param name="id"></param>
        public static bool Ken_Del(Ken model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@KenID", model.KenID);
                    p.Add("@UserID", model.CreateUserID);
                    conn.Execute("Ken_Del", p, commandType: CommandType.StoredProcedure);
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
