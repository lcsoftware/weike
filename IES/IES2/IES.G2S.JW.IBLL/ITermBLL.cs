using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.IBLL
{
    public interface ITermBLL
    {
        #region  列表
        List<Term> Term_List(Term model, int PageSize, int PageIndex);
        #endregion

        #region 新增

        Term Term_ADD(Term model);

        #endregion 

        #region 删除
        bool Term_Del(Term model);
        #endregion
    }
}
