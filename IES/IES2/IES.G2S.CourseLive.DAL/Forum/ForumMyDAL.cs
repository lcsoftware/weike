using IES.CC.Forum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.DataBase;
using Dapper;
using System.Data;

namespace IES.G2S.CourseLive.Forum.DAL
{
    /// <summary>
    /// 我关注的
    /// </summary>
    public class ForumMyDAL
    {

        #region 属性修改
        /// <summary>
        /// 为论坛主题或回复点赞  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool ForumMy_IsGood_Upd(ForumMy model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@TopicID", model.TopicID);
                    p.Add("@ResponseID", model.ResponseID);
                    p.Add("@UserID", model.UserID);
                    conn.Execute("ForumMy_IsGood_Upd", p, commandType: CommandType.StoredProcedure);
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
