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
    public class NewsDAL
    {
        #region 列表
        public static List<News> News_List(News model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Title", model.Title);
                    p.Add("@StartDate", model.Startdate);
                    p.Add("@EndDatef", model.Enddatef);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);

                    return conn.Query<News>("News_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region  新增
        public static News News_ADD(News model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Title", model.Title);
                    p.Add("@Content", model.Content);
                    p.Add("@SectionID", model.Sectionid);
                    p.Add("@Sectionchild", model.Sectionchild);
                    p.Add("@CreateDate", model.Createdate);
                    p.Add("@EndDate", model.Enddate);
                    p.Add("@Click", model.Clicks);
                    p.Add("@IsImportant", model.Isimportant);
                    p.Add("@IsTop", model.Istop);
                    p.Add("@SysID", model.Sysid);
                    p.Add("@ModuleID", model.Moduleid);
                    p.Add("@OrganizationID", model.Organizationid);

                    conn.Execute("News_ADD", p, commandType: CommandType.StoredProcedure);
                    model.Newsid = p.Get<int>("NewsID");
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
        public static bool News_Del(News model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NewsID", model.Newsid);
                    conn.Execute("News_Del", p, commandType: CommandType.StoredProcedure);
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

        public static bool News_Upd(News model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NewsID", model.Newsid);
                    p.Add("@Title", model.Title);
                    p.Add("@Content", model.Content);
                    p.Add("@SectionID", model.Sectionid);
                    p.Add("@Sectionchild", model.Sectionchild);
                    p.Add("@CreateDate", model.Createdate);
                    p.Add("@EndDate", model.Enddate);
                    p.Add("@IsImportant", model.Isimportant);
                    p.Add("@IsTop", model.Istop);

                    conn.Execute("News_Upd", p, commandType: CommandType.StoredProcedure);
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
