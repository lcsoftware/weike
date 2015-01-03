using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.CC.OC.Model;

namespace IES.G2S.OC.IBLL.Class
{
    public interface IClassBLL
    {




        #region  列表

        //   List<TeachingClass> 

        #endregion


        #region 详细信息

        #endregion


        #region  新增

        bool TeachingClassInfo_ADD(TeachingClassInfo model);


        #endregion


        #region 对象更新
        bool TeachingClassInfo_Upd(TeachingClassInfo model);


        #endregion


        #region 单个批量更新




        #endregion


        #region 属性批量操作




        #endregion


        #region 删除

        /// <summary>
        /// 删除教学班
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool TeachingClass_Del(TeachingClass model);


        /// <summary>
        /// 删除授课教师
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool TeachingClassTeacher_Del(TeachingClassTeacher model);

        /// <summary>
        /// 删除教学班学生
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool TeachingClassStudent_Del(TeachingClassStudent model);

        #endregion

    }
}
