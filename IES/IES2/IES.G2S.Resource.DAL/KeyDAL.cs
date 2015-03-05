using Dapper;
using IES.DataBase;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.DAL
{
    public class KeyDAL
    {

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Key> Key_List(Key model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);

                    return conn.Query<Key>("Key_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }


        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Key> Resource_Key_List(int ocid, string searchKey, string source, int userId, int topNum)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", ocid);
                    p.Add("@SearchKey", searchKey);
                    p.Add("@Source", source);
                    p.Add("@UserID", userId);
                    p.Add("@TopNum", topNum);

                    return conn.Query<Key>("Resource_Key_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取文件、习题相关有效关键字 
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <param name="Source"></param>
        /// <param name="UserID"></param>
        /// <param name="TopNum"></param>
        /// <returns></returns>
        public static List<Key> ExerciseOrFile_Key_List(string SearchKey, string Source, int UserID, int TopNum,int OCID)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@SearchKey", SearchKey);
                    p.Add("@Source", Source);
                    p.Add("@UserID", UserID);
                    p.Add("@TopNum", TopNum);

                    return conn.Query<Key>("Resource_Key_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Key>();
            }
        }

    
        
    }
}
