using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.DataBase;
using Dapper;
using IES.Resource.Model;

namespace IES.G2S.Resource.DAL
{
    public class ExerciseDAL
    {

        public static Exercise Exercise_Get(int id)
        {
            using (var conn = DbHelper.ResourceService())
            {
                var p = new DynamicParameters();
                p.Add("@ExerciseID", id);
                return conn.Query<Exercise>("Exercise_Get", p , commandType: CommandType.StoredProcedure).First();
            }
        }


        public  static void Exercise_Del( int id )
        {
            using (var conn = DbHelper.ResourceService())
            {
                var p = new DynamicParameters();
                p.Add("@ExerciseID", id );
                conn.Execute("Exercise_Del", p , commandType: CommandType.StoredProcedure);
            }
        }

        public static void Exercise_Del(Exercise model)
        {
            using (var conn = DbHelper.ResourceService())
            {
                var p = new DynamicParameters();
                p.Add("@ExerciseID", model.ExerciseID);
                conn.Execute("Exercise_Del", p , commandType: CommandType.StoredProcedure);
            }
        }

        public static void Exercise_ADD(Exercise  model)
        {



        }  



    }
}
