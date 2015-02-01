using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.JW.Model;

namespace IES.G2S.JW.DAL
{
    public class MailServerDAL
    {

        #region 列表
        /// <summary>
        /// 获取列表
        /// </summary>
        public static List<MailServer> MailServer_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<MailServer>("MailServer_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 编辑或新增
        /// <summary>
        /// 
        /// </summary>
        public static MailServer MailServer_Edit(MailServer model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ServerID", model.ServerID);
                    p.Add("@SMTPServer",model.SMTPServer);
                    p.Add("@Port",model.Port);
                    p.Add("@Account",model.Account);
                    p.Add("@Password",model.Password);
                    p.Add("@IsSSL",model.IsSSL);
                    p.Add("@op_MailID", "",dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("MailServer_Edit", p, commandType: CommandType.StoredProcedure);
                    model.op_MailID = p.Get<int>("@op_MailID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return new MailServer();
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public static bool MailServer_Del(MailServer model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ServerID", model.ServerID);
                    conn.Execute("MailServer_Del", p, commandType: CommandType.StoredProcedure);
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
