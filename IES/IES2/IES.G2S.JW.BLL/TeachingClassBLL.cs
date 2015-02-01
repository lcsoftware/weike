using IES.G2S.JW.DAL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class TeachingClassBLL
    {
        #region  列表
        public List<TeachingClass> TeachingClass_List(TeachingClass model, int PageIndex, int PageSize)
        {
            return TeachingClassDAL.TeachingClass_List(model, PageIndex, PageSize);
        }

        //获取添加的学生信息
        public List<TeachingClassStudent> TeachingClassStudent_List(int TeachingClassID, string StudentIDs, int PageIndex, int PageSize)
        {
            return TeachingClassDAL.TeachingClassStudent_List(TeachingClassID, StudentIDs, PageIndex, PageSize);
        }

        //新增学生
        public TeachingClassStudent TeachingClassStudent_Edit(TeachingClassStudent model )
        {
            return TeachingClassDAL.TeachingClassStudent_Edit(model);
        }
        #endregion

        #region  详细信息

        public TeachingClassInfo TeachingClassInfo_Get(TeachingClassInfo model)
        {
            return TeachingClassDAL.TeachingClassInfo_Get(model);
        }

        #endregion

        #region  新增

        public TeachingClass TeachingClass_ADD(TeachingClass model)
        {
            return TeachingClassDAL.TeachingClass_ADD(model);
        }

        #endregion

        #region  对象更新
        public bool TeachingClass_Upd(TeachingClass model)
        {
            return TeachingClassDAL.TeachingClass_Upd(model);
        }
        #endregion

        #region 单个对象更新

        #endregion

        #region 批量删除


        #endregion

        #region 删除

        public bool TeachingClass_Del(TeachingClass model)
        {
            return TeachingClassDAL.TeachingClass_Del(model);
        }

        #endregion
    }
}
