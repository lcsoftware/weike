using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IChapterBLL
    {

        #region  列表


        #endregion

        #region 详细信息

        #endregion

        #region  新增


        bool Chapter_ADD(Chapter model);


        #endregion

        #region 对象更新

        bool Chapter_Upd(Chapter model);

        #endregion

        #region 单个批量更新





        #endregion

        #region 属性批量操作




        #endregion

        #region 删除

        bool Chapter_Del(Chapter model);

        #endregion

    }
}
