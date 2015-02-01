using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface IUserBLL
    {
        #region 列表
        List<User> User_List(User model, int PageIndex, int PageSize);

        #endregion

        #region 详细信息

        User User_Get(User model);

        #endregion

        #region  新增
        User User_ADD(User model);

        #endregion

        #region 删除
        bool User_Del(User model);

        #endregion

        #region 更新
        bool User_Upd(User model);

        #endregion
    }
}
