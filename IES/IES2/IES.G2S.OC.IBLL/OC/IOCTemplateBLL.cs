using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.IBLL.OC
{
    public interface IOCTemplateBLL
    {

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.Model.OC.OCTemplate> OCTemplate_List();
    }
}
