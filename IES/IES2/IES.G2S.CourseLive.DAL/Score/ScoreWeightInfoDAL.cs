using IES.CC.Model.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.DataBase;
using Dapper;
using System.Data;

namespace IES.G2S.CourseLive.DAL.Score
{
    public class ScoreWeightInfoDAL
    {
        #region 列表
        public static List<ScoreWeightInfo> ScoreWeightInfo_List(int? teachingClassID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", teachingClassID);
                    return conn.Query<ScoreWeightInfo>("ScoreWeightInfo_List", p, commandType: CommandType.StoredProcedure).ToList(); ;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
