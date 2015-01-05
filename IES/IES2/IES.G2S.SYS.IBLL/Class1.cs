using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.SYS.Model;

namespace IES.G2S.SYS.IBLL
{
    public interface Class1
    {
        #region 列表
        List<User> User_List(User model, int PageIndex, int PageSize);

        #endregion

        #region  新增
        bool User_ADD(User model);

        #endregion

        #region 删除
        bool User_Del(User model);

        #endregion

        #region 更新
        bool User_Upd(User model);

        #endregion
    }
}
