using Dapper;
using IES.CC.OC.Model;
using IES.DataBase;
using IES.JW.Model;
using IES.SYS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.DAL.FC
{
    public class FCDAL
    {
        /// <summary>
        /// 翻转课堂组合信息
        /// </summary>
        /// <param name="FCID"></param>
        /// <returns></returns>
        public static OCFCInfo OCFCInfo_Get(int FCID)
        {
            using (var conn = DbHelper.CCService())
            {
                OCFCInfo ci = new OCFCInfo();
                var p = new DynamicParameters();
                p.Add("@FCID", FCID);
                //return conn.Query<OCFCInfo>("OCFC_List", p, commandType: CommandType.StoredProcedure).ToList();
                var multi = conn.QueryMultiple("OCFCInfo_Get", p, commandType: CommandType.StoredProcedure);
                var ocfc = multi.Read<OCFC>().Single();
                var studentlist = multi.Read<User>().ToList();
                var grouplist = multi.Read<OCFCGroup>().ToList();
                var teachingclasslist = multi.Read<TeachingClass>().ToList();
                var teacher = multi.Read<User>().Single();

                ci.fcLiveGroupCount = grouplist.Count;
                ci.fcStudentCount = studentlist.Count;
                ci.fcteacherName = teacher.UserName;
                ci.fcTeachingClass = teachingclasslist;
                ci.fcUserList = studentlist;
                ci.fcTeacher = teacher;
                ci.ocfc = ocfc;
                ci.fcGroupList = grouplist;
                return ci;
            }
        }

        /// <summary>
        /// 根据课程id获取此课程下所有翻转课堂的教学班
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public static List<OCFCClass> OCFCClass_OCID_Get(int OCID)
        {
            List<OCFCClass> fcclasslist = new List<OCFCClass>();
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID",OCID);
                fcclasslist = conn.Query<OCFCClass>("OCFCClass_OCID_List",p,commandType: CommandType.StoredProcedure).ToList();
                return fcclasslist;
            }
           
        }

        /// <summary>
        /// |根据翻转课程id获取翻转课堂的教学班相信信息    
        /// </summary>
        /// <param name="FCID">翻转课程id</param>
        /// <returns></returns>
        public static List<TeachingClass> OCFCClass_FCID_Get(int FCID) {
            return null;
        }

        /// <summary>
        /// 翻转课堂基础信息列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static List<OCFC> OCFC_List(int OCID, int UserID)
        {
            List<OCFC> ocfclist = new List<OCFC>();
            using (var conn = DbHelper.CCService())
            {
                var p = new DynamicParameters();
                p.Add("@OCID", OCID);
                p.Add("@UserID",UserID);
                ocfclist = conn.Query<OCFC>("OCFC_List", p, commandType: CommandType.StoredProcedure).ToList();
                return ocfclist;
            }
        }
    }
}
