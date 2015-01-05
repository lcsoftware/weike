using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class CourseBLL:ICourseBLL
    {
        #region  列表

        public List<Course> Course_List(String Key,Course model, int PageIndex, int PageSize)
        {
            return CourseDAL.Course_List(Key,model,PageIndex, PageSize );
        }

        #endregion

        #region  新增

        public Course Course_ADD(Course model)
        {
            return CourseDAL.Course_ADD(model);
        }
        #endregion

        #region  对象更新

        public bool Course_Upd(Course model)
        {
            return CourseDAL.Course_Upd(model);
        }

        #endregion

        #region 删除

        public bool Course_Del(Course model)
        {
            return CourseDAL.Course_Del(model);
        }

        #endregion
    }
}
