using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Dapper;
using Dapper.Tvp;
using IES.DataBase;
using IES.Resource.Model;


namespace IES.G2S.Resource.DAL
{
    public class FileDAL
    {

        #region 文件夹

        public static Folder Folder_Get()
        {
            return new Folder();
        }


        public static Folder Folder_GetModel(Folder folder)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FolderID", folder.FolderID);
                    return conn.Query<Folder>("Folder_Get", p, commandType: CommandType.StoredProcedure).SingleOrDefault<Folder>();
                }
            }
            catch (Exception e)
            {
                return new Folder();
            }
        }

        #region  列表
        public static List<Folder> Folder_List(Folder model)
        {

            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@UserID", model.CreateUserID);
                    return conn.Query<Folder>("Folder_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Folder>();
            }
        }

        #endregion

        #region 详细信息

        #endregion 

        #region  新增
        public static Folder   Folder_ADD(Folder model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FolderID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CreateUserID", model.CreateUserID);
                    p.Add("@OwnerUserID", model.OwnerUserID);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@FolderName", model.FolderName);
                    p.Add("@Brief", model.Brief);

                    conn.Execute("Folder_ADD", p, commandType: CommandType.StoredProcedure);
                    model.FolderID = p.Get<int>("FolderID");

                    return model;
                }
            }
            catch (Exception e)
            {
                return null  ;
            }



        }



        #endregion

        #region 对象更新
        public static bool Folder_ShareRange_Upd(Folder model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FolderID);
                    p.Add("@ShareRange", model.ShareRange);
                    conn.Execute("Folder_ShareRange_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool Folder_Batch_ShareRange(string folderIDS, int shareRange)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileIDS", folderIDS);
                    p.Add("@ShareRange", shareRange);
                    conn.Execute("Folder_Batch_ShareRange", p, commandType: CommandType.StoredProcedure);
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

        /// <summary>
        /// 文件夹重命名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Folder_Name_Upd(Folder model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FolderID", model.FolderID);
                    p.Add("@FolderName", model.FolderName);
                    conn.Execute("Folder_Name_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 文件夹移动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Folder_ParentID_Upd(Folder model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FolderID", model.FolderID);
                    p.Add("@ParentID", model.ParentID);
                    conn.Execute("Folder_ParentID_Upd", p, commandType: CommandType.StoredProcedure);
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



        public static bool Folder_Batch_Del(List<Folder> folderlist)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {

                    var recordparam = new List<SqlDataRecord>();
                    var recordcolumn = new[]
                    {
                        new SqlMetaData("id", SqlDbType.Int)
                    };
                    foreach (var r in folderlist)
                    {
                        var record = new SqlDataRecord(recordcolumn);
                        record.SetInt32(0, r.FolderID);
                        recordparam.Add(record);
                    }

                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = "@IDS";
                    parameter.SqlDbType = System.Data.SqlDbType.Structured;
                    parameter.Value = recordparam;
                    
                    conn.Execute("Folder_Batch_Del", parameter , commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 文件夹删除
        /// </summary>
        /// <param name="id"></param>
        public static bool Folder_Del(Folder model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FolderID", model.FolderID);
                    conn.Execute("Folder_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion 

        #endregion

        #region  文件

        #region  列表

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<File> File_Search(File file, int PageSize, int PageIndex)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Searchkey", file.FileTitle);
                    p.Add("@OCID", file.OCID);
                    p.Add("@CourseID", file.CourseID);
                    p.Add("@FolderID", file.FolderID);
                    p.Add("@FileType", file.FileType);
                    p.Add("@UploadTime", file.UploadTime);
                    p.Add("@ShareRange", file.ShareRange);
                    p.Add("@UserID", file.CreateUserID);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<File>("File_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<File>();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<File> File_Search(File file)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Searchkey", file.FileTitle);
                    p.Add("@OCID", file.OCID);
                    p.Add("@CourseID", file.CourseID);
                    p.Add("@FolderID", file.FolderID);
                    p.Add("@FileType", file.FileType);
                    p.Add("@UploadTime", file.UploadTime);
                    p.Add("@ShareRange", file.ShareRange);
                    p.Add("@UserID", file.CreateUserID);
                    //p.Add("@PageSize", PageSize);
                    //p.Add("@PageIndex", PageIndex);
                    return conn.Query<File>("File_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<File>();
            }

        }

        #endregion

        #region 详细信息

        #endregion

        #region  新增




        #endregion

        #region 对象更新

        public static File File_ADD(File model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@FolderID", model.FolderID);
                    p.Add("@CreateUserID", model.CreateUserID);
                    p.Add("@CreateUserName", model.CreateUserName);
                    p.Add("@OwnerUserID", model.OwnerUserID);
                    p.Add("@FileTitle", model.FileTitle);
                    p.Add("@FileName", model.FileName);
                    p.Add("@Ext", model.Ext);
                    p.Add("@FileType", model.FileType);
                    p.Add("@FileSize", model.FileSize);
                    p.Add("@pingyin", model.pingyin);
                    p.Add("@ServerID", model.ServerID);

                    conn.Execute("File_ADD", p, commandType: CommandType.StoredProcedure);
                    model.FolderID = p.Get<int>("FileID");

                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        #endregion

        #region 单个属性更新

        /// <summary>
        /// 文件共享范围设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool File_ShareRange_Upd(File model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FileID );
                    p.Add("@ShareRange", model.ShareRange);
                    conn.Execute("File_ShareRange_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 文件移动设置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool File_FolderID_Upd(File model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FileID);
                    p.Add("@FolderID", model.FolderID);
                    conn.Execute("File_FolderID_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool File_FileTitle_Upd(File model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FileID);
                    p.Add("@FileTitle", model.FileTitle );
                    conn.Execute("File_FileTitle_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool File_Chapter_Ken_Edit(File model, Chapter chapter, Ken ken)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FileID);
                    p.Add("@ChapterID", chapter.ChapterID);
                    p.Add("@KenID", ken.KenID);
                    conn.Execute("File_Chapter_Ken_Edit", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool File_Key_Edit(File model, Key key, Ken ken)
        {

            return true;
        }

        public static bool File_Ken_Edit(File model, Key key, Ken ken)
        {

            return true;
        }

        #endregion

        #region 属性批量操作

        public static bool File_Batch_ShareRange(string FileIDS, int ShareRange)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileIDS", FileIDS);
                    p.Add("@ShareRange", ShareRange);
                    conn.Execute("File_Batch_ShareRange", p, commandType: CommandType.StoredProcedure);
                    return true;
                }

                //using (var conn = DbHelper.ResourceService())
                //{
                //    var recordparam = new List<SqlDataRecord>();
                //    var recordcolumn = new[]
                //    {
                //        new SqlMetaData("id", SqlDbType.Int)
                //    };
                //    foreach (var r in filelist)
                //    {
                //        var record = new SqlDataRecord(recordcolumn);
                //        record.SetInt32(0, r.FileID);
                //        recordparam.Add(record);
                //    }

                //    var ids = new TableValueParameter("@ids", "IDList", recordparam);                    
                //    conn.Execute("File_Batch_ShareRange", ids, commandType: CommandType.StoredProcedure);
                //    return true;
                //}
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool File_Batch_FolderID(List<File> filelist)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var recordparam = new List<SqlDataRecord>();
                    var recordcolumn = new[]
                    {
                        new SqlMetaData("id", SqlDbType.Int)
                    };
                    foreach (var r in filelist)
                    {
                        var record = new SqlDataRecord(recordcolumn);
                        record.SetInt32(0, r.FileID);
                        recordparam.Add(record);
                    }

                    var ids = new TableValueParameter("@ids", "IDList", recordparam);
                    conn.Execute("File_Batch_FolderID", ids, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool File_Batch_Del( List<File> filelist )
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var recordparam = new List<SqlDataRecord>();
                    var recordcolumn = new[]
                    {
                        new SqlMetaData("id", SqlDbType.Int)
                    };
                    foreach ( var r in filelist )
                    {
                        var record = new SqlDataRecord(recordcolumn);
                        record.SetInt32(0, r.FileID);
                        recordparam.Add(record);
                    }

                    var ids = new TableValueParameter("@ids", "IDList", recordparam);
                    conn.Execute("File_Batch_Del", ids, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        #endregion

        #region 删除


        public static string File_Del(File model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@FileID", model.FileID);
                    p.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("File_Del", p, commandType: CommandType.StoredProcedure);
                    return p.Get<string>("Result");
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }



        #endregion


        #endregion 

    }
}
