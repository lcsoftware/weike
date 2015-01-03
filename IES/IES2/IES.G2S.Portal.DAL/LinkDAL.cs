using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Portal.Model;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;

namespace IES.G2S.Portal.DAL
{
    public class LinkDAL
    {
        #region 列表
        public static List<Link> Link_List(Link model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();                
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);

                    return conn.Query<Link>("Link_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region  新增
        public static Link Link_ADD(Link model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LinkID", model.Linkid);
                    p.Add("@Title", model.Title);
                    p.Add("@URL", model.Url);
                    p.Add("@SysID", model.Sysid);
                    p.Add("@ModuleID", model.Moduleid);
                    p.Add("@OrganizationID", model.Organizationid);
                    p.Add("@IsIMG", model.Isimg);
                    p.Add("@AttachmentID", model.Attachmentid);
                    p.Add("@Clicks", model.Clicks);

                    conn.Execute("Link_ADD", p, commandType: CommandType.StoredProcedure);
                    model.Linkid = p.Get<int>("LinkID");
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }



        #endregion

        #region 删除
        public static bool Link_Del(Link model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LinkID", model.Linkid);
                    conn.Execute("Link_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region 更新

        public static bool Link_Upd(Link model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@LinkID", model.Linkid);
                    p.Add("@Title", model.Title);
                    p.Add("@URL", model.Url);
                    p.Add("@IsIMG", model.Isimg);
                    p.Add("@AttachmentID", model.Attachmentid);

                    conn.Execute("Link_Upd", p, commandType: CommandType.StoredProcedure);
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
