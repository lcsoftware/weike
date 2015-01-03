using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL;
using IES.G2S.OC.IBLL.FC;
using IES.G2S.OC.DAL.FC;
using IES.JW.Model;


namespace IES.G2S.OC.BLL.FC
{
    public class FCBLL : IFCBLL
    {
        #region  列表

        /// <summary>
        /// 翻转课堂组合信息列表
        /// </summary>
        /// <param name="OCID">课程id</param>
        /// <param name="Userid">用户id</param>
        /// <returns></returns>
        public List<OCFCInfo> OCFCInfo_List(int OCID,int Userid)
        {
            List<OCFC> ocfcList = OCFC_List(OCID, Userid);
            List<OCFCInfo> ocfcInfoList = new List<OCFCInfo>();
            OCFCInfo ocfcInfo = new OCFCInfo();
            int fcID = 0;
            foreach (var item in ocfcList)
            {
                fcID = item.FCID;
                if (fcID != 0 && fcID != null)
                {
                    ocfcInfo = OCFCInfo_Get(item.FCID);
                    ocfcInfoList.Add(ocfcInfo);
                }
            }
            return ocfcInfoList;
        }

        /// <summary>
        /// 根据课程id获取翻转课堂教学班
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<OCFCClass> OCFCClass_Get(int OCID)
        {
            List<OCFCClass> classlist = null;
            return null;
        }

        /// <summary>
        /// 翻转课堂信息
        /// </summary>
        /// <param name="FCID"></param>
        /// <returns></returns>
        public OCFCInfo OCFCInfo_Get(int FCID)
        {
            OCFCInfo ocfcInfo = FCDAL.OCFCInfo_Get(FCID);
            return ocfcInfo;
        }

        /// <summary>
        /// 翻转课堂基础信息列表
        /// </summary>
        /// <param name="OCID">课程id</param>
        /// <param name="UserID">用户id（此属性待定）</param>
        /// <returns></returns>
        public List<OCFC> OCFC_List(int OCID, int UserID)
        {
            return FCDAL.OCFC_List(OCID,UserID);
        }
        #endregion
    }
}
