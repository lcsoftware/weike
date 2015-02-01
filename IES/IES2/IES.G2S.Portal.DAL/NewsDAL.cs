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
                    p.Add("@Key", model.Key);
                    p.Add("@SectionID", model.SectionID);
                    p.Add("@StartTime", model.StartTime);
                    p.Add("@EndTime", model.EndTime);
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
                    p.Add("@SectionID", model.SectionID);
                    p.Add("@CreateDate", model.CreateDate);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@IsImportant", model.IsImportant);
                    p.Add("@IsTop", model.IsTop);
                    p.Add("@SysID", model.SysID);
                    p.Add("@OrganizationID", model.OrganizationID);
                    conn.Execute("News_ADD", p, commandType: CommandType.StoredProcedure);
                    model.NewsID = p.Get<int>("NewsID");
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
                    p.Add("@NewsID", model.NewsID);
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

        #region 批量删除
        public static bool News_Batch_Del(string IDS)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();
                    p.Add("@NewsIDS", IDS);
                    conn.Execute("News_Batch_Del", p, commandType: CommandType.StoredProcedure);
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
                    p.Add("@NewsID", model.NewsID);
                    p.Add("@Title", model.Title);
                    p.Add("@Content", model.Content);
                    p.Add("@SectionID", model.SectionID);
                    p.Add("@CreateDate", model.CreateDate);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@IsImportant", model.IsImportant);
                    p.Add("@IsTop", model.IsTop);
                    p.Add("@SysID", model.SysID);
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

        #region 详细信息
        public static News News_Get(News model)
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {                  
                    var p = new DynamicParameters();
                    p.Add("@NewsID", model.NewsID);
                    return conn.Query<News>("News_Get", p, commandType: CommandType.StoredProcedure).Single();                                 
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region 新闻公告所属板块
        public static List<NewsSection> NewsSection_List()
        {
            try
            {
                using (var conn = DbHelper.PortalService())
                {
                    var p = new DynamicParameters();

                    return conn.Query<NewsSection>("Section_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
