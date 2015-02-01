using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.OC;
using IES.CC.Model.OC;
using IES.G2S.OC.DAL;

namespace IES.G2S.OC.BLL.OC
{
   public class OCTemplateBLL:IOCTemplateBLL
    {
       /// <summary>
        /// 获取模板列表
       /// </summary>
       /// <returns></returns>
       public List<OCTemplate> OCTemplate_List() {
           return OCTemplateDAL.OCTemplate_List();
       }
    }
}
