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
using IES.Cache;


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
        //public List<OCFCInfo> OCFCInfo_List(int OCID,int Userid)
        //{
        //    List<OCFCInfo> ocfcInfoList = new List<OCFCInfo>();
        //    ICache cache = CacheFactory.Create();
        //    if (!cache.Exists(Userid.ToString()+"_"+OCID, "OCFCInfoList"))
        //    {
        //        List<OCFC> ocfcList = OCFC_List(OCID, Userid);
        //        OCFCInfo ocfcInfo = new OCFCInfo();
        //        int fcID = 0;
        //        foreach (var item in ocfcList)
        //        {
        //            fcID = item.FCID;
        //            if (fcID != 0)
        //            {
        //                ocfcInfo = OCFCInfo_Get(item.FCID);
        //                ocfcInfoList.Add(ocfcInfo);
        //            }
        //        }

        //        if (ocfcInfoList != null)
        //        {
        //            ocfcInfoList = ocfcInfoList.OrderByDescending(i => i.ocfc.UpdateTime).ToList();
        //        }

        //        cache.Set(Userid.ToString() + "_" + OCID, "OCFCInfoList", ocfcInfoList);
        //    }
        //    else
        //    {
        //        ocfcInfoList = cache.Get<List<OCFCInfo>>(Userid.ToString() + "_" + OCID, "OCFCInfoList");
        //    }
            
        //    return ocfcInfoList;
        //}


        /// <summary>
        /// 翻转课堂信息
        /// </summary>
        /// <param name="FCID"></param>
        /// <returns></returns>
        public OCFCInfo OCFCInfo_Get(int FCID)
        {
            OCFCInfo ocfcInfo = new OCFCInfo();
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(FCID.ToString(), "OCFCInfo_Get"))
            {
                ocfcInfo = FCDAL.OCFCInfo_Get(FCID);

                cache.Set(FCID.ToString(), "OCFCInfo_Get", ocfcInfo);
            }
            else
            {
                ocfcInfo = cache.Get<OCFCInfo>(FCID.ToString(), "OCFCInfo_Get");
            }
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
            List<OCFC> ocfcList = FCDAL.OCFC_List(OCID, UserID);
            return ocfcList;
        }

        /// <summary>
        /// 获取翻转课堂资料列表
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public List<OCFCFile> OCFCFile_List(OCFC fc)
        {
            return FCDAL.OCFCFile_List(fc);
        }

        /// <summary>
        /// 翻转课堂互动任务列表
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public List<OCFCLive> OCFCLive_List(OCFC fc)
        {
            return FCDAL.OCFCLive_List(fc);
        }

        /// <summary>
        /// 获取单个翻转课堂
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public OCFC OCFC_Get(OCFC fc) {
            return FCDAL.OCFC_Get(fc);
        }

        #endregion

        #region 新增编辑
        /// <summary>
        /// 编辑或新增翻转课堂主内容
        /// </summary>
        /// <param name="ocfc"></param>
        /// <returns></returns>
        public int OCFC_ADDorEdit(OCFC fc)
        {

            if (fc.FCID == null || fc.FCID == 0)
            {
                return FCDAL.OCFC_ADD(fc);
            }
            else
            {
                return FCDAL.OCFC_Edit(fc);
            }
        }

        /// <summary>
        /// 教学资料设为必读
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool OCFCFile_Must(OCFCFile file)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除教学资料
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool OCFCFile_Del(OCFCFile file)
        {
            return FCDAL.OCFCFile_Del(file);
        }
        /// <summary>
        /// 删除论题互动
        /// </summary>
        /// <param name="live"></param>
        /// <returns></returns>
        public bool OCFCLive_Del(OCFCLive live)
        {
            return FCDAL.OCFCLive_Del(live);
        }

        
        #endregion
    }
}
