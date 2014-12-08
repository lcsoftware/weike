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

        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Folder Folder_ADD(Folder model)
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
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@ShareRange", model.ShareRange);
                    p.Add("@Brief", model.Brief);

                    conn.Execute("Folder_ADD", p, commandType: CommandType.StoredProcedure);
                    model.FolderID = p.Get<int>("FolderID");

                    return model;
                }
            }
            catch (Exception e)
            {
                return model;
            }



        }

        /// <summary>
        /// 文件夹删除
        /// </summary>
        /// <param name="id"></param>
        public static bool Folder_Del(Folder model )
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
                return false ;
            }
        }


        public static bool Folder_Batch_Del( List<Folder> folderlist )
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

                    var ids = new TableValueParameter("@ids", "IDList", recordparam );
                    conn.Execute("Folder_Batch_Del", ids, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Folder> Folder_List(Folder model)
        {

            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@ShareRange", model.ShareRange);
                    p.Add("@UserID", model.CreateUserID);
                    return conn.Query<Folder>("Folder_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Folder>();
            }
        }

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

    }
}
