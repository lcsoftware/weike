using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;

namespace IES.G2S.JW.IBLL
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
