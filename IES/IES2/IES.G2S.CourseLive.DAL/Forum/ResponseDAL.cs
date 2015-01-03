using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IES.CC.Forum.Model;
using IES.DataBase;
using Dapper;

namespace IES.G2S.CourseLive.DAL.Forum
{
    public class ResponseDAL
    {
        #region  列表


        #endregion


        #region 详细信息

        #endregion


        #region  新增


        /// <summary>
        /// 添加论题回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ForumResponse ForumResponse_ADD(ForumResponse model)
        {
            return new ForumResponse();
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
