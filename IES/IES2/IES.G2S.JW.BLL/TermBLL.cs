using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class TermBLL:ITermBLL
    {
        #region 列表

        public List<Term> Term_List(Term model, int PageSize, int PageInde)
        {
            return TermDAL.Term_List(model, PageSize, PageInde);
        }

        #endregion

        #region 新增

        public Term Term_ADD(Term model)
        {
            return TermDAL.Term_ADD(model);
        }

	    #endregion

        #region 删除

        public bool Term_Del(Term model)
        {
            return TermDAL.Term_Del(model);
        }

        #endregion

    }
}
