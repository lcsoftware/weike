using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using IES.CC.Model.PBL;
using IES.DataBase;
using IES.JW.Model;
using Dapper;


namespace IES.G2S.CourseLive.DAL.PBL
{
    public class GroupDAL
    {        
        #region  列表


        
        /// <summary>
        /// 获取分组模式列表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static GroupModeInfo GroupModeInfo_List(GroupTask model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@UserID", model.UserID);


                    var multi = conn.QueryMultiple("GroupModeInfo_List", p, commandType: CommandType.StoredProcedure);


                    var groupmodelist = multi.Read<GroupMode>().ToList();
                    var teachinglist = multi.Read<TeachingClass>().ToList();
                    var grouplist  = multi.Read<Group>().ToList();


                    GroupModeInfo groumodeinfo = new GroupModeInfo();
                    groumodeinfo.groupmodelist = groupmodelist;
                    groumodeinfo.Grouplist = grouplist;
                    groumodeinfo.teachingclasslist = teachinglist;

                    return groumodeinfo;


                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        #endregion


        #region 详细信息

        #endregion


        #region  新增




        #endregion


        #region 对象更新



        #endregion


        #region 单个批量更新





        #endregion


        #region 属性批量操作




        #endregion


        #region 删除


        #endregion
    }
}
