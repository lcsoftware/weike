using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.JW.DAL;
using IES.JW.Model;

namespace IES.G2S.JW.BLL
{
    public class CourseTypeBLL 
    {

        #region 课程分类
        public List<Coursetype> CourseType_Tree()
        {
            return CourseTypeDAL.CourseType_Tree();
        }
        #endregion

        #region 上级课程分类
        public List<Coursetype> CourseType_P_List()
        {
            return CourseTypeDAL.CourseType_P_List();
        }
        #endregion

        #region 编辑
        public Coursetype CourseType_Edit(Coursetype model)
        {
            return CourseTypeDAL.CourseType_Edit(model);
        }
        #endregion

        #region 删除
        public bool CourseType_Del(Coursetype model)
        {
            return CourseTypeDAL.CourseType_Del(model);
        }
        #endregion

        #region 取消删除
        public bool CourseType_CancelDel(Coursetype model)
        {
            return CourseTypeDAL.CourseType_CancelDel(model);
        }
        #endregion

    }
}
