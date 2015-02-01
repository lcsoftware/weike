using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using Dapper;
using IES.DataBase;
using System.Data;

namespace IES.G2S.JW.DAL
{
    public class OrganizationDAL
    {
        #region  列表
        /// <summary>
        /// 获取教学组织的列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Organization> Organization_List(Organization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    //p.Add("@ParentID", model.ParentID);
                    return conn.Query<Organization>("Organization_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Organization>();
            }
        }
        /// <summary>
        /// 获取教学组织的列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<ShortOranization> Organization_S_List(ShortOranization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OrganizationIDs", model.OrganizationIDs);
                    return conn.Query<ShortOranization>("Organization_S_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<ShortOranization>();
            }
        }
        #endregion

        #region 详细信息

        public static OrganizationInfo OrganizationInfo_Get(IOrganization model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.JWService())
                {
                    OrganizationInfo of = new OrganizationInfo();
                    var p = new DynamicParameters();
                    p.Add("@OrganizationID", model.OrganizationID);
                    var multi = conn.QueryMultiple("OrganizationInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var organization = multi.Read<Organization>().Single();
                    var organizationtype = multi.Read<OrganizationType>().Single();
                    of.organizationcommon.organization = organization;
                    of.organizationcommon.organizationtype = organizationtype;
                    return of;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region  新增

        /// <summary>
        /// 新增/编辑教学组织
        /// </summary>
        public static Organization Organization_Edit(Organization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@OrganizationNo", model.OrganizationNo);
                    p.Add("@OrganizationName", model.OrganizationName);
                    p.Add("@OrganizationNameEn", model.OrganizationNameEn);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@OrganizationTypeID", model.OrganizationTypeID);
                    p.Add("@Introduction", model.Introduction);
                    p.Add("@IntroductionEn", model.IntroductionEn);
                    p.Add("@IsShow", model.IsShow);
                    p.Add("@Link", model.Link);
                    p.Add("@IsTeaching", model.IsTeaching);
                    p.Add("@LinkStatus", model.LinkStatus);
                    p.Add("@op_OrganizationID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("Organization_Edit", p, commandType: CommandType.StoredProcedure);
                    model.OrganizationID = p.Get<int>("op_OrganizationID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

        #region 对象更新

        public static bool Organization_Upd(Organization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@OrganizationNo", model.OrganizationNo);
                    p.Add("@OrganizationName", model.OrganizationName);
                    p.Add("@OrganizationNameEn", model.OrganizationNameEn);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@OrganizationTypeID", model.OrganizationTypeID);
                    p.Add("@Introduction", model.Introduction);
                    conn.Execute("Organization_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
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
        public static bool Organization_Del(Organization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OrganizationID", model.OrganizationID);
                    conn.Execute("Organization_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region  获取全部信息

        public static List<Organization> NewsOrganization_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();

                    return conn.Query<Organization>("NewsOrganization_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion


        //取消删除
        public static bool Organization_CancelDel(Organization model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OrganizationID", model.OrganizationID);
                    conn.Execute("Organization_CancelDel", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 移动组织机构
        /// </summary>
        /// <returns></returns>
        public static bool Organization_Move(int SelfID, int OptionID, string type)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SelfID", SelfID);
                    p.Add("@OptionID", OptionID);
                    p.Add("@Type", type);
                    conn.Execute("Organization_Move", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
