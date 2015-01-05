using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;
using IES.JW.Model;

namespace IES.G2S.OC.IBLL.FC
{
    public interface IFCBLL
    {

        /// <summary>
        /// 翻转课堂基础信息列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        List<OCFC> OCFC_List(int OCID, int UserID);

        /// <summary>
        /// 根据课程id获取翻转课堂教学班
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        List<OCFCClass> OCFCClass_Get(int OCID);
        
        /// <summary>
        /// 翻转课堂信息
        /// </summary>
        /// <param name="FCID"></param>
        /// <returns></returns>
        OCFCInfo OCFCInfo_Get(int FCID);

        
    }
}
