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
    /// <summary>
    /// 试卷库
    /// </summary>
    public class PaperDAL
    {

        #region  列表

        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <param name="Searchkey">关键字</param>
        /// <param name="CourseID">课程编号</param>
        /// <param name="PaperType">试卷类型</param>
        /// <param name="Scope">适用范围</param>
        /// <param name="UploadTime">时间范围</param>
        /// <param name="ShareRange">共享范围</param>
        /// <param name="UserID">用户编号</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<Paper> Paper_Search(Paper paper, int PageSize, int PageIndex)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@Searchkey", paper.Papername);
                    p.Add("@OCID", paper.OCID);
                    p.Add("@CourseID", paper.CourseID);
                    p.Add("@UserID", paper.CreateUserID);
                    p.Add("@Type", paper.Type);
                    p.Add("@Scope", paper.Scope);
                    p.Add("@UpdateTime", paper.UpdateTime);
                    p.Add("@ShareScope", paper.ShareScope);

                    p.Add("@PageSize", PageSize);
                    p.Add("@PageIndex", PageIndex);
                    return conn.Query<Paper>("Paper_Search", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<Paper>();
            }

        }
        #endregion

        #region 详细信息

        /// <summary>
        /// 创建不同的试卷信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IPaper CreatePaper(Paper model)
        {
            return new Paper();

        }


        /// <summary>
        /// 获取试卷的基本信息；获取自测型试卷信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static Paper Paper_Get(Paper model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@PaperID", model.PaperID);
                    return conn.Query<Paper>("Paper_Get", p, commandType: CommandType.StoredProcedure).SingleOrDefault<Paper>();

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取试卷的组卷策略详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static PaperDefineInfo PaperDefineInfo_Get(Paper model)
        {

            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    PaperDefineInfo pd = new PaperDefineInfo();
                    var p = new DynamicParameters();
                    p.Add("@PaperID", model.PaperID);

                    var multi = conn.QueryMultiple("PaperDefineInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var paper = multi.Read<Paper>().Single();

                    var grouplist = multi.Read<PaperGroup>().ToList();
                    var attachmentlist = multi.Read<Attachment>().ToList();
                    var paperexerciselist = multi.Read<PaperExercise>().ToList();
                    var papertacticlist = multi.Read<PaperTactic>().ToList();
                    pd.PaperID = paper.PaperID;
                    pd.Type = paper.Type;
                    pd.paper = paper;
                    pd.papergrouplist = grouplist;
                    pd.attachmentlist = attachmentlist;
                    pd.exerciselist = paperexerciselist;
                    pd.papertacticlist = papertacticlist;

                    return pd;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 获取智能型试卷的详细信息，即生成好的试卷信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static PaperInfo PaperInfo_Get(Paper model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    PaperInfo pi = new PaperInfo();
                    var p = new DynamicParameters();
                    p.Add("@PaperID", model.PaperID);

                    var multi = conn.QueryMultiple("PaperInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var paper = multi.Read<Paper>().Single();
                    var grouplist = multi.Read<PaperGroup>().ToList();
                    var attachmentlist = multi.Read<Attachment>().ToList();
                    var paperexerciselist = multi.Read<PaperExercise>().ToList();

                    pi.PaperID = paper.PaperID;
                    pi.Type = paper.Type;
                    pi.paper = paper;
                    pi.papergrouplist = grouplist;
                    pi.attachmentlist = attachmentlist;
                    pi.exerciselist = paperexerciselist;

                    return pi;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取答题卡试卷的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static PaperCardInfo PaperCardInfo_Get(Paper model)
        {
            try
            {
                using (IDbConnection conn = DbHelper.ResourceService())
                {
                    PaperCardInfo pc = new PaperCardInfo();
                    var p = new DynamicParameters();
                    p.Add("@PaperID", model.PaperID);

                    var multi = conn.QueryMultiple("PaperCardInfo_Get", p, commandType: CommandType.StoredProcedure);
                    var paper = multi.Read<Paper>().Single();
                    var papercardexerciselist = multi.Read<PaperCardexercise>().ToList();
                    pc.PaperID = paper.PaperID;
                    pc.Type = paper.Type;
                    pc.paper = paper;
                    pc.papercardexerciselist = papercardexerciselist;


                    return pc;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region  新增

        /// <summary>
        /// 试卷基本信息添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Paper_ADD(Paper model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {



                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 添加答题卡试卷信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool PaperCardInfo_ADD(PaperCardInfo model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {



                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 试卷结构添加，分组、分组的组卷策略，分组中的习题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool PaperDefineInfo_ADD(PaperDefineInfo model)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {



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
        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="paperid">试卷编号</param>
        /// <param name="userid">操作用户编号</param>
        public static bool Paper_Del(Paper paper)
        {
            try
            {
                using (var conn = DbHelper.ResourceService())
                {
                    var p = new DynamicParameters();
                    p.Add("@PaperID", paper.PaperID);
                    conn.Execute("Paper_Del", p, commandType: CommandType.StoredProcedure);
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
