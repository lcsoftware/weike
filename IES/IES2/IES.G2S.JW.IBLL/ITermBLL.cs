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
        List<Term> Term_List(Term model);
        /// <summary>
        /// 获取校历信息列表
        /// </summary>
        /// <returns></returns>
        List<TermInfo> TermInfo_List();
        #endregion

        #region 新增或修改

        Term Term_Edit(Term model);

        #endregion 

        #region 删除
        bool Term_Del(Term model);
        #endregion
    }
}
