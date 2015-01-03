using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using IES.CC.Model.PBL;
using IES.DataBase;
using Dapper;


namespace IES.G2S.CourseLive.DAL.PBL
{
    public class GroupTaskDAL
    {
        #region  列表

        /// <summary>
        /// 小组任务列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<GroupTask> GroupTask_List(GroupTask model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@UserID", model.UserID);
                    return conn.Query<GroupTask>("GroupTask_List", p, commandType: CommandType.StoredProcedure).ToList();




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

        /// <summary>
        /// 添加小组任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool GroupTask_ADD(GroupTask model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();

                    p.Add("@TaskID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@TaskName", model.TaskName);
                    p.Add("@ResultRequire", model.ResultRequire);
                    p.Add("@TermsID", model.TermsID);
                    p.Add("@StartDate", model.StartDate);
                    p.Add("@SubmitTime", model.SubmitTime);
                    p.Add("@EndDate", model.EndDate);
                    p.Add("@Introduction", model.Introduction);
                    p.Add("@UserID", model.UserID);
                    p.Add("@GroupModeID", model.GroupModeID);
                    p.Add("@IsAllowSeeOtherGroup", model.IsAllowSeeOtherGroup);
                    p.Add("@IsOCTask", model.IsOCTask);

                    conn.Execute("GroupTask_ADD", p, commandType: CommandType.StoredProcedure);
                    model.TaskID = p.Get<int>("TaskID");

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



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
