using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.SYS.Model;

namespace IES.G2S.SYS.IBLL
{
    public interface ILogBLL
    {
        #region 列表
        List<Log> Log_List(Log model, int PageIndex, int PageSize);
        #endregion
    }
}
