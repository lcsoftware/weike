﻿using System;
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
    public class ChapterDAL
    {

        #region 列表

        /// <summary>
        /// 获取章节树形列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Chapter> Chapter_List(Chapter model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    return conn.Query<Chapter>("Chapter_List", p, commandType: CommandType.StoredProcedure).ToList();
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


        #region 新增

        /// <summary>
        /// 章节新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool  Chapter_ADD(Chapter model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@CourseID", model.CourseID);
                    p.Add("@OwnerUserID", model.OwnerUserID);
                    p.Add("@CreateUserID", model.CreateUserID); 
                    p.Add("@Title", model.Title );
                    p.Add("@ParentID", model.ParentID );
                    conn.Execute("Chapter_ADD", p, commandType: CommandType.StoredProcedure);
                    model.ChapterID = p.Get<int>("ChapterID");
                    return true;
                }
            }
            catch (Exception e)
            {
                return false ;
            }

        }


        #endregion 


        #region 更新

        /// <summary>
        /// 章节更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Chapter_Upd(Chapter model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@Title", model.Title);
                    conn.Execute("Chapter_Upd", p, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }


        #endregion 


        #region 删除 
        /// <summary>
        ///  章节删除
        /// </summary>
        /// <param name="id"></param>
        public static bool Chapter_Del(Chapter model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", model.ChapterID);
                    conn.Execute("Chapter_Del", p, commandType: CommandType.StoredProcedure);
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