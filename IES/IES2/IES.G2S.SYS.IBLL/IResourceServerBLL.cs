using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.SYS.Model;

namespace IES.G2S.SYS.IBLL
{
    public interface IResourceServerBLL
    {
        #region 列表
        List<ResourceServer> ResourceServer_List();

        #endregion
        #region 删除
        bool ResourceServer_Del(ResourceServer model);

        #endregion
        #region 新增
        ResourceServer ResourceServer_ADD(ResourceServer model);

        #endregion
    }
}
