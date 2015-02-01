using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface ICourseBLL
    {
        #region  列表

        List<Course> Course_List(Course model, int PageSize, int PageIndex);

        #endregion

        #region 详细信息
        #endregion

        #region  新增或修改

        Course Course_Edit(Course model);

        #endregion      

        #region 删除

        bool Course_Del(Course model);

        #endregion
    }
}
