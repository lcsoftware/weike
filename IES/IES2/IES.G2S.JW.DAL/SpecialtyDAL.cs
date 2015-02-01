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
    /// <summary>
    /// 专业管理 Specialty，SpecialtySite， SpecialtyTeacher， SpecialtyType
    /// </summary>
    public  class SpecialtyDAL
    {
        #region  列表

        public static List<Specialty> Specialty_List(Specialty model, int PageIndex, int PageSize)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Key", model.Key);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SchoolingLength", model.SchoolingLength);
                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<Specialty>("Specialty_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Specialty>();
            }
        }

        public static List<ShortSpecialty> Specialty_Short_List(ShortSpecialty model)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyIDs", model.SpecialtyIDs);
                    return conn.Query<ShortSpecialty>("Specialty_Short_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<ShortSpecialty>();
            }
        }
        #endregion

        #region 详细信息

        public static SpecialtyInfo SpecialtyInfo_Get(SpecialtyInfo model)
        {
            try
            {
                using (IDbConnection conn=DbHelper.JWService())
                {
                    SpecialtyInfo sf = new SpecialtyInfo();
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    var multi = conn.QueryMultiple("SpecialtyInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var specialty = multi.Read<Specialty>().Single();
                    var specialtysitelist = multi.Read<SpecialtySite>().ToList();
                    var specialtyteacherlist = multi.Read<SpecialtyTeacher>().ToList();
                    sf.specialty = specialty;
                    sf.specialtysitelist = specialtysitelist;
                    sf.specialtyteacherlist = specialtyteacherlist;

                    return sf;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 对象修改或新增

        public static Specialty Specialty_Edit(Specialty model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@SpecialtyNo", model.SpecialtyNo);
                    p.Add("@SpecialtyName", model.SpecialtyName);
                    p.Add("@SpecialtyNameEn", model.SpecialtyNameEn);
                    p.Add("@SchoolingLength", model.SchoolingLength);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SpecialtyTypeID", model.SpecialtyTypeID);
                    p.Add("@Introduction", model.Introduction);
                    p.Add("@IsShow", model.IsShow);
                    p.Add("@Output","", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("@op_SpecialtyID","", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    conn.Execute("Specialty_Edit", p, commandType: CommandType.StoredProcedure);
                    model.Output = p.Get<string>("@Output");
                    model.op_SpecialtyID = p.Get<int>("@op_SpecialtyID");
                    return model;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 单个批量更新





        #endregion

        #region 批量删除
        public static bool Specialty_Batch_Del(string IDS)
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyIDS", IDS);
                    conn.Execute("Specialty_Batch_Del", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        #endregion

        #region 删除

        public static bool Specialty_Del(Specialty model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyID", model.SpecialtyID);
                    conn.Execute("Specialty_Del", p, commandType: CommandType.StoredProcedure);
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

        public static List<Specialty> NewsSpecialty_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();

                    return conn.Query<Specialty>("NewsSpecialty_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region 学科树
        public static List<SpecialtyType> SpecialtyType_Tree_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<SpecialtyType>("SpecialtyType_Tree", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<SpecialtyType>();
            }
        }
        #endregion

        #region 上级学科
        public static List<SpecialtyType> SpecialtyType_P_List()
        {
            try
            {
                using (var conn = DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    return conn.Query<SpecialtyType>("SpecialtyType_P_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception)
            {
                return new List<SpecialtyType>();
            }
        }
        #endregion
    }
}
