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

        public static List<Specialty> Specialty_List(Specialty model,int PageSize,int PageIndex)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    //p.Add("@SpecialtyID", model.SpecialtyID);
                    p.Add("@SpecialtyNo", model.SpecialtyNo);
                    p.Add("@SpecialtyName", model.SpecialtyName);
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

        #endregion

        #region 详细信息

        public static SpecialtyInfo SpecialtyInfo_Get(ISpecialty model)
        {
            try
            {
                using (IDbConnection conn=DbHelper.JWService())
                {
                    SpecialtyInfo sf = new SpecialtyInfo();
                    var p = new DynamicParameters();
                    p.Add("@Specialty", model.SpecialtyID);
                    var multi = conn.QueryMultiple("SpecialtyInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var specialty = multi.Read<Specialty>().Single();
                    var specialtylist = multi.Read<Specialty>().ToList();
                    var specialtysitelist = multi.Read<SpecialtySite>().ToList();
                    var specialtyteacherlist = multi.Read<SpecialtyTeacher>().ToList();
                    sf.specialtycommon.specialty = specialty;
                    sf.specialtycommon.specialtylist = specialtylist;
                    sf.specialtycommon.specialtysitelist = specialtysitelist;
                    sf.specialtycommon.specialtyteacherlist = specialtyteacherlist;

                    return sf;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region  新增

        public static Specialty Specialty_ADD(Specialty model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@SpecialtyID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@SpecialtyNo", model.SpecialtyNo);
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@SpecialtyName", model.SpecialtyName);
                    p.Add("@SpecialtyNameEn", model.SpecialtyNameEn);
                    p.Add("@SchoolingLength", model.SchoolingLength);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SpecialtyTypeID", model.SpecialtyTypeID);
                    p.Add("@Introduction", model.Introduction);
                    conn.Execute("Specialty_ADD", p, commandType: CommandType.StoredProcedure);
                    model.SpecialtyID = p.Get<int>("SpecialtyID");
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

        public static bool Specialty_Upd(Specialty model)
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
                    p.Add("@ParentID", model.ParentID);
                    p.Add("@SchoolingLength", model.SchoolingLength);
                    p.Add("@OrganizationID", model.OrganizationID);
                    p.Add("@SpecialtyTypeID", model.SpecialtyTypeID);
                    p.Add("@Introduction", model.Introduction);
                    conn.Execute("Specialty_Upd", p, commandType: CommandType.StoredProcedure);
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

        public static bool Specialty_Del(Specialty model)
        {
            try
            {
                using (var conn=DbHelper.JWService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Specialty", model.SpecialtyID);
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
    }
}
