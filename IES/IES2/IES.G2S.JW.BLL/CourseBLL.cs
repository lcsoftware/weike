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

        public List<Course> Course_List(Course model, int PageIndex, int PageSize)
        {
            return CourseDAL.Course_List( model, PageIndex, PageSize);
        }

        #endregion

        #region  对象新增或取消

        public Course Course_Edit(Course model)
        {
            return CourseDAL.Course_Edit(model);
        }

        #endregion

        #region 删除

        public bool Course_Del(Course model)
        {
            return CourseDAL.Course_Del(model);
        }

        #endregion       

        #region  获取详细信息

        public Course Course_Get(int CourseID)
        {
            return CourseDAL.Course_Get(CourseID);
        }

        #endregion

        #region 批量删除
        public bool Course_Batch_Del(string IDS)
        {
            return CourseDAL.Course_Batch_Del(IDS);
        }

        #endregion

        
    }
}
