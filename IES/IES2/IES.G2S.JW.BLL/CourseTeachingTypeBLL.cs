using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.JW.DAL;
using IES.JW.Model;
using IES.G2S.JW.IBLL;

namespace IES.G2S.JW.BLL
{
    public class CourseTeachingTypeBLL 
    {

        #region 列表
        public List<CourseTeachingType> CourseTeachingType_List()
        {
            return CourseTeachingTypeDAL.CourseTeachingType_List();
        }
        #endregion

        #region 新增
        public CourseTeachingType CourseTeachingType_ADD(CourseTeachingType model)
        {
            return CourseTeachingTypeDAL.CourseTeachingType_ADD(model);
        }
        #endregion

        #region 更新
        public bool CourseTeachingType_Upd(CourseTeachingType model)
        {
            return CourseTeachingTypeDAL.CourseTeachingType_Upd(model);
        }
        #endregion

        #region 删除
        public bool CourseTeachingType_Del(CourseTeachingType model)
        {
            return CourseTeachingTypeDAL.CourseTeachingType_Del(model);
        }
        #endregion

    }
}
