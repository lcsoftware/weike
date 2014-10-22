/* **************************************************************
  * Copyright(c) 2014 Nevupo, All Rights Reserved.    
  * File             : AppBLL.cs
  * Description      : 对DbContextHolderBase的简单包装，调用ClownFish底层接口访问数据
  * Author           : Zhaotianyu 
  * Created          : 2014-09-25
  * Revision History : 
******************************************************************/
namespace App.Score.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClownFish;

    /// <summary>
    /// 数据底层访问定义
    /// </summary>
    public class AppBLL : DbContextHolderBase
    { 
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="proc">存储过程</param>
        /// <param name="inputParams">参数</param>
        /// <returns>返回影响行数</returns>        
        public int ExecuteNonQuery(string proc, object inputParams)
        {            
            this.DbContext.CreateCommand(proc, System.Data.CommandType.StoredProcedure);
            this.DbContext.SetCommandParameters(inputParams);
            return this.DbContext.ExecuteNonQuery();
        }

        /// <summary>
        /// 返回单个数据实体类
        /// </summary>
        /// <typeparam name="T">泛型数据</typeparam>
        /// <param name="proc">存储过程名称</param>
        /// <param name="inputParams">参数</param>
        /// <returns>数据实体对象</returns> 
        public T GetDataItem<T>(string proc, object inputParams) where T : class, new()
        {
            this.DbContext.CreateCommand(proc, System.Data.CommandType.StoredProcedure);
            this.DbContext.SetCommandParameters(inputParams);
            return this.DbContext.GetDataItem<T>(); 
        }

        /// <summary>
        /// 返回数据集
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="inputParams">参数</param>
        /// <returns>数据集</returns>
        public System.Data.DataSet FillDataSet(string proc, object inputParams)
        {
            this.DbContext.CreateCommand(proc, System.Data.CommandType.StoredProcedure);
            this.DbContext.SetCommandParameters(inputParams);
            return this.DbContext.FillDataSet();
        }

        /// <summary>
        /// 返回泛型数据表列表
        /// </summary>
        /// <typeparam name="T">泛型数据</typeparam>
        /// <param name="proc">存储过程名称</param>
        /// <param name="inputParams">参数</param>>
        /// <returns>数据列表</returns>
        public List<T> FillList<T>(string proc, object inputParams) where T : class, new()
        {            
            this.DbContext.CreateCommand(proc, System.Data.CommandType.StoredProcedure);
            this.DbContext.SetCommandParameters(inputParams);
            return this.DbContext.FillList<T>();
        }

        /// <summary>
        /// 返回数据列表
        /// </summary>
        /// <typeparam name="T">泛型数据</typeparam>
        /// <param name="sqlText">Sql语句</param>
        /// <param name="inputParams">参数</param>
        /// <returns>数据列表</returns>
        public List<T> FillListByText<T>(string sqlText, object inputParams) where T : class, new()
        {
            return DbHelper.FillList<T>(sqlText, inputParams, CommandKind.SqlTextWithParams);
            //this.DbContext.CreateCommand(sqlText, System.Data.CommandType.Text);
            //this.DbContext.SetCommandParameters(inputParams);
            //return this.DbContext.FillList<T>();
        }
        /// <summary>
        /// 返回数据表
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        public System.Data.DataTable FillDataTableByText(string sqlText, object inputParams)
        {
            return DbHelper.FillDataTable(sqlText, inputParams, CommandKind.SqlTextWithParams);
        }

        public int ExecuteNonQueryByText(string sqlText, object inputParams) {
            return DbHelper.ExecuteNonQuery(sqlText, inputParams, CommandKind.SqlTextWithParams);
        }
        public int ExecuteNonQueryByText(string sqlText) {
            return DbHelper.ExecuteNonQuery(sqlText, null, CommandKind.SqlTextNoParams);
        }
        /// <summary>
        /// 返回数据表
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public System.Data.DataTable FillDataTableByText(string sqlText)
        {
            return DbHelper.FillDataTable(sqlText, null, CommandKind.SqlTextNoParams);
        }

        /// <summary>
        /// 返回数据表(DataTable)
        /// </summary>
        /// <param name="proc">存储过程名称</param>
        /// <param name="inputParams">参数</param>>
        /// <returns>数据表</returns>
        public System.Data.DataTable FillDataTable(string proc, object inputParams)
        {                        
            this.DbContext.CreateCommand(proc, System.Data.CommandType.StoredProcedure);
            this.DbContext.SetCommandParameters(inputParams); 
            return this.DbContext.FillDataTable();
        }
    }
}
