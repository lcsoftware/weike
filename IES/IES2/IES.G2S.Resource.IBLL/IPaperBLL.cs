using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IPaperBLL
    {
        /// <summary>
        /// 试卷查询
        /// </summary>
        /// <param name="Searchkey">关键字</param>
        /// <param name="CourseID">课程编号</param>
        /// <param name="PaperType">试卷类型</param>
        /// <param name="Scope">适用范围</param>
        /// <param name="UploadTime">更新时间</param>
        /// <param name="ShareRange">共享范围</param>
        /// <param name="UserID">试卷拥有人</param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        List<Paper> Paper_Search(string Searchkey, int CourseID, int PaperType, int Scope , DateTime UploadTime, int ShareRange, int UserID, int PageSize, int PageIndex );



    }
}
