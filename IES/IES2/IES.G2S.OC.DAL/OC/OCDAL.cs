using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

using IES.CC.OC.Model;

using Dapper;
using IES.DataBase;



namespace IES.G2S.OC.DAL
{
     public class OCDAL
    {


         /// <summary>
        /// 获取用户的在线课程列表
         /// </summary>
         /// <param name="userid"></param>
         /// <param name="role"></param>
         /// <returns></returns>
       public static List<IES.CC.OC.Model.OC> OC_List(int userid, int role)
       {
           using (var conn = DbHelper.CCService())
           {
               var p = new DynamicParameters();
               p.Add("@userid", userid);
               p.Add("@role", role);
               return conn.Query<IES.CC.OC.Model.OC>("OC_List", p, commandType: CommandType.StoredProcedure).ToList();


           }
       }


        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public static int OCSiteColumn_ADD(OCSiteColumn column)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", column.ColumnID, DbType.Int32, ParameterDirection.InputOutput);
                p.Add("@OCID", column.OCID);
                p.Add("@UserID", column.UserID);
                p.Add("@ParentID", column.ParentID);
                p.Add("@Title", column.Title);
                p.Add("@ContentType", column.ContentType);

                conn.Execute("OCSiteColumn_ADD", p, commandType: CommandType.StoredProcedure);
                return p.Get<int>("ColumnID");
               
            }
        }
         /// <summary>
        /// 网站显示风格更新
         /// </summary>
         /// <param name="SiteID"></param>
         /// <param name="DisplayStyle"></param>
         /// <returns></returns>
        public static bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@SiteID", SiteID);
                p.Add("@DisplayStyle", DisplayStyle);
                return Convert.ToBoolean(conn.Execute("OCSite_DisplayStyle_Upd", p, commandType: CommandType.StoredProcedure));

            }
        }
         /// <summary>
        /// 获取网站的栏目列表
         /// </summary>
         /// <param name="OCID"></param>
         /// <param name="UserID"></param>
         /// <returns></returns>
        public static List<OCSite> OCSite_Get(int OCID, int UserID)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID", UserID);
                return conn.Query<OCSite>("OCSite_Get", p, commandType: CommandType.StoredProcedure).ToList();

            }
        }
         /// <summary>
        /// 获取网站的所有栏目列表
         /// </summary>
         /// <param name="OCID"></param>
         /// <param name="UserID"></param>
         /// <returns></returns>
        public static List<OCSiteColumn> OCSiteColumn_Tree(int OCID, int UserID)
        {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID", UserID);
                return conn.Query<OCSiteColumn>("OCSiteColumn_Tree", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }
         /// <summary>
        /// 网站显示语言更新
         /// </summary>
         /// <param name="SiteID"></param>
         /// <param name="Language"></param>
        public static void OCSite_Language_Upd(int SiteID, int Language) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@SiteID", SiteID);
                p.Add("@Language", Language);
                conn.Execute("OCSite_Language_Upd", p, commandType: CommandType.StoredProcedure);
               
            }
        }
         /// <summary>
        /// 更新课程网站的建设模式
         /// </summary>
         /// <param name="OCID"></param>
         /// <param name="BuildMode"></param>
         /// <param name="OutSiteLink"></param>
        public static void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@BuildMode", BuildMode);
                p.Add("@OutSiteLink", OutSiteLink);
                conn.Execute("OCSite_BuildMode_Upd", p, commandType: CommandType.StoredProcedure);
            }
        }
         /// <summary>
        /// 网站栏目的启用
         /// </summary>
         /// <param name="OCID"></param>
         /// <param name="FileldType"></param>
        public static void OCSite_Field_Upd(int OCID, string FileldType) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@FileldType", FileldType);
                conn.Execute("OCSite_Field_Upd", p, commandType: CommandType.StoredProcedure);
            } 
        }
         /// <summary>
         /// 网站栏目内容更新
         /// </summary>
         /// <param name="ColumnID"></param>
         /// <param name="Conten"></param>
        public static void OCSiteColumn_Conten_Upd(int ColumnID, string Conten) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                p.Add("@Conten", Conten);
                conn.Execute("OCSiteColumn_Conten_Upd", p, commandType: CommandType.StoredProcedure);
            }  
        }
         /// <summary>
        /// 删除栏目
         /// </summary>
         /// <param name="ColumnID"></param>
        public static void OCSiteColumn_Del(int ColumnID) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                conn.Execute("OCSiteColumn_Del", p, commandType: CommandType.StoredProcedure);
            }  
        }
         /// <summary>
        /// 获取网站的栏目下子栏目列表
         /// </summary>
         /// <param name="ColumnID"></param>
         /// <param name="UserID"></param>
         /// <returns></returns>
        public static List<OCSiteColumn> OCSiteColumn_List(int ColumnID, int UserID) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                p.Add("@UserID", UserID);
                return conn.Query<OCSiteColumn>("OCSiteColumn_List", p, commandType: CommandType.StoredProcedure).ToList();
            }  
        }
         /// <summary>
        /// 更新栏目
         /// </summary>
         /// <param name="ColumnID"></param>
         /// <param name="Title"></param>
         /// <param name="ContentType"></param>
        public static void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                p.Add("@Title", Title);
                p.Add("@ContentType", ContentType);
                conn.Execute("OCSiteColumn_Upd", p, commandType: CommandType.StoredProcedure);
            } 
        }

         /// <summary>
        /// 更新父栏目
         /// </summary>
         /// <param name="ColumnID"></param>
         /// <param name="ParentID"></param>
        public static void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                p.Add("@ParentID", ParentID);
                conn.Execute("OCSiteColumn_ParentID_Upd", p, commandType: CommandType.StoredProcedure);
            }  
        }
         /// <summary>
        /// 更新网站栏目的顺序 (Direction: orderup ,  orderdown , levelup , leveldown)
         /// </summary>
         /// <param name="ColumnID"></param>
         /// <param name="Direction"></param>
        public static void OCSiteColumn_Move(int ColumnID, string Direction) {
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@ColumnID", ColumnID);
                p.Add("@Direction", Direction);
                conn.Execute("OCSiteColumn_Move", p, commandType: CommandType.StoredProcedure);
            }      
        }





      






    }

    


}
