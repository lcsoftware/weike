using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace IES.Common
{
    /// <summary>
    /// 提供将List集合转换成数据表功能类
    /// </summary>
    public static class ListToDateUtil
    {
        /// <summary>
        /// 将List转换成数据表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> objList) where T : class,new()
        {
            DataTable dt = new DataTable();
            Type type = typeof(T);
            PropertyInfo[] pros = type.GetProperties();
            foreach (var rowName in pros)
            {
                dt.Columns.Add(rowName.Name);
            }
            DataRow dr = null ;
            foreach (var obj in objList)
            {
                dr = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    dr[col] = type.GetProperty(col.ColumnName).GetValue(obj);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 将List转化成DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataSet ListToDataSet<T>(List<T> list) where T : class,new()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDateUtil.ListToDataTable<T>(list));
            return ds;
        }
    }
}
