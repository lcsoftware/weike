using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IES.JW.Model;
using Dapper;
using IES.DataBase;

namespace IES.G2S.JW.DAL
{
    public class ParmInfoDAL
    {
        #region 获取查询条件
        public static ParmInfo Parm_Info_List()
        {
            using (var conn = DbHelper.JWService())
            {
                ParmInfo parm = new ParmInfo();
                var p = new DynamicParameters();
                var multi = conn.QueryMultiple("Parm_Info_List", p, commandType: CommandType.StoredProcedure);
                var crty = multi.Read<Coursetype>().ToList();
                var org = multi.Read<Organization>().ToList();
                var tr = multi.Read<TermType>().ToList();
                var tyer = multi.Read<Term>().ToList();
                var crtchty = multi.Read<CourseTeachingType>().ToList();
                var cls = multi.Read<Class>().ToList();
                var ety = multi.Read<User>().ToList();
                var schlen = multi.Read<Specialty>().ToList();
                var spty = multi.Read<SpecialtyType>().ToList();
                var sp = multi.Read<Specialty>().ToList();
                var aro = multi.Read<AuRole>().ToList();
                var sys = multi.Read<Sys>().ToList();
                var CfgSchool = multi.Read<CfgSchool>().ToList();
                parm.crtylist = crty;
                parm.orglist = org;
                parm.trlist = tr;
                parm.tyerlist = tyer;
                parm.crtchtylist = crtchty;
                parm.clslist = cls;
                parm.etylist = ety;
                parm.schlenlist = schlen;
                parm.sptylist = spty;
                parm.splist = sp;
                parm.arolist = aro;
                parm.syslist = sys;
                parm.cfglist = CfgSchool;
                return parm;
            }
        }
        #endregion

        #region 根据一级学科获取二级学科
        public static List<SpecialtyType> SpecialtyTyep2_List(SpecialtyType model)
        {
            using (var conn = DbHelper.JWService())
            {
                SpecialtyType spty2 = new SpecialtyType();
                var p = new DynamicParameters();
                p.Add("@ParentID", model.ParentID);
                return conn.Query<SpecialtyType>("SpecialtyTyep2_List", p, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        #endregion
    }
}
