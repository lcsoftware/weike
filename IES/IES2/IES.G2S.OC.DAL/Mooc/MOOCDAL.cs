using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IES.CC.OC.Model;
using IES.DataBase;

namespace IES.G2S.OC.DAL.Mooc
{
    public class MOOCDAL
    {
        #region  详细信息
        /// <summary>
        /// 获取网站的栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static OCMooc OCMooc_Get(int OCID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    return conn.Query<OCMooc>("OCMooc_Get", p, commandType: CommandType.StoredProcedure).Single();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region  列表信息
        public static List<OCMoocOffline> OCMoocOffline_List(int OCID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    return conn.Query<OCMoocOffline>("OCMoocOffline_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public static List<OCMoocFile> OCMoocFile_List(int OCID, int ChapterID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", OCID);
                    p.Add("@ChapterID", ChapterID);
                    return conn.Query<OCMoocFile>("OCMoocFile_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public static List<OCMoocLive> OCMoocLiveDiscuss_List(int ChapterID)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@ChapterID", ChapterID);
                    return conn.Query<OCMoocLive>("OCMoocLiveDiscuss_List", p, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 新增
        public static OCMoocLive OCMoocLive_Add(OCMoocLive model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@MoocLiveID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@OCID", model.OCID);
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@SourceID", model.SourceID);
                    p.Add("@Source", model.Source);
                    p.Add("@IsMust", model.IsMust);
                    p.Add("@IsDiscuss", model.IsDiscuss);
                    model.MoocLiveID = p.Get<int>("MoocLiveID");
                    return model;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region  更新
        /// <summary>
        /// 编辑资料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void OCMoocFile_Edit(OCMoocFile model)
        {
            try
            {
                using (var conn = DbHelper.CCService())
                {
                    var p = new DynamicParameters();
                    p.Add("@OCID", model.OCID);
                    p.Add("@ChapterID", model.ChapterID);
                    p.Add("@FileID", model.FileID);
                    p.Add("@Timelimit", model.Timelimit);
                    p.Add("@IsMust", model.IsMust);
                    conn.Execute("OCMoocFile_Edit", p, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
