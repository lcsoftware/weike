using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface IClassBLL
    {
        #region  列表

        List<Class> Class_List(Class model, int PageIndex, int PageSize);

        #endregion

        #region  详细信息

        Class Class_Get(int ClassID);

        List<User> ClassStudent_List(int ClassID);

        #endregion


        #region  对象修改或删除

        Class Class_Edit(Class model);

        #endregion

        #region 单个对象更新
        #endregion

        #region 批量删除
        bool Class_Batch_Del(string IDS);


        #endregion

        #region 删除

        bool Class_Del(Class model);

        #endregion
    }
}
