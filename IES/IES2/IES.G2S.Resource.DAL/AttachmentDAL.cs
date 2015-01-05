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
    public  class AttachmentDAL
    {

        public static List<Attachment> Attachment_List(Attachment model)
        {
            try
            {
                using (var conn = DbHelper.CommonService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SourceID", model.SourceID);
                    p.Add("@Source", model.Source);
                    return conn.Query<Attachment>("Attachment_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
  
        public static bool Attachment_ADD(Attachment model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@AttachmentID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@ServerID", model.ServerID );
                    p.Add("@FileName", model.FileName );
                    p.Add("@Title", model.Title);
                    p.Add("@FileSize", model.FileSize);
                    p.Add("@Source", model.Source);
                    p.Add("@SourceID", model.SourceID);
                    p.Add("@Guid", model.Guid);
                    p.Add("@RefFileID", model.RefFileID);
                    conn.Execute("Attachment_ADD", p, commandType: CommandType.StoredProcedure);
                    model.AttachmentID = p.Get<int>("AttachmentID");
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Attachment_SourceID_Upd(Attachment model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Guid", model.Guid);
                    p.Add("@SourceID", model.SourceID);
                    conn.Execute("Attachment_SourceID_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool Attachment_Del( Attachment model )
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@AttachmentID", model.AttachmentID );
                    conn.Execute("Attachment_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
