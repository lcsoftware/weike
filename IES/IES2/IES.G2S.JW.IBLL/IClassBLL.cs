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

        List<Class> Class_List(Class model, int UserID, int PageIndex, int PageSize);

        #endregion

        #region  详细信息


        #endregion

        #region  新增

        Class Class_Add(Class model );

        #endregion

        #region  对象更新

        bool Class_Upd(Class model , out string opt );

        #endregion

        #region 单个对象更新
        #endregion

        #region 批量属性操作
        #endregion

        #region 删除

        bool Class_Del(Class model);

        #endregion
    }
}
