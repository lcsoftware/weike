using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.OC;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL;
using IES.Security;
using IES.Cache;

namespace IES.G2S.OC.BLL.OC
{
   public class OCBLL:IOCBLL
   {



       #region  列表


       public  List<IES.CC.OC.Model.OC> OC_List(int userid , int role )
       {
           ICache cache = CacheFactory.Create();

           if ( !cache.Exists( userid.ToString() , "OC_List" ) )
           {
                 List<IES.CC.OC.Model.OC>  oclist =  OCDAL.OC_List(userid, role);
                 cache.Set( userid.ToString() , "OC_List", oclist );
                 return oclist ;
           }
           else
           {
                 return cache.Get<List<IES.CC.OC.Model.OC>>( userid.ToString() , "OC_List" );
           }
       }

       #endregion 

       /// <summary>
        ///   添加栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public  int OCSiteColumn_Edit(OCSiteColumn column) {
            return OCDAL.OCSiteColumn_Edit(column);
            
        }
       /// <summary>
        /// 网站显示风格更新
       /// </summary>
       /// <param name="SiteID"></param>
       /// <param name="DisplayStyle"></param>
       /// <returns></returns>
        public  bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle) {
            return OCDAL.OCSite_DisplayStyle_Upd(SiteID, DisplayStyle);
        }
        /// <summary>
        /// 获取网站的栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public  List<OCSite> OCSite_Get(int OCID, int UserID) {
            return OCDAL.OCSite_Get(OCID,UserID);
        }
       /// <summary>
        /// 获取网站的所有栏目列表
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
        public List<OCSiteColumn> OCSiteColumn_Tree(int OCID, int UserID) {
            return OCDAL.OCSiteColumn_Tree(OCID, UserID);
        }
       /// <summary>
        /// 网站显示语言更新
       /// </summary>
       /// <param name="SiteID"></param>
       /// <param name="Language"></param>
        public void OCSite_Language_Upd(int SiteID, int Language) {
            OCDAL.OCSite_Language_Upd(SiteID, Language);
        }
       /// <summary>
        /// 更新课程网站的建设模式
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="BuildMode"></param>
       /// <param name="OutSiteLink"></param>
        public void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink) {
            OCDAL.OCSite_BuildMode_Upd(OCID, BuildMode, OutSiteLink);
        }
       /// <summary>
        /// 网站栏目的启用
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="FileldType"></param>
        public void OCSite_Field_Upd(int OCID, string FileldType) {
            OCDAL.OCSite_Field_Upd(OCID,FileldType);
        }
       /// <summary>
        /// 网站栏目内容更新
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <param name="Conten"></param>
        public void OCSiteColumn_Conten_Upd(int ColumnID, string Conten) {
            OCDAL.OCSiteColumn_Conten_Upd(ColumnID,Conten);
        }
       /// <summary>
        /// 删除栏目
       /// </summary>
       /// <param name="ColumnID"></param>
        public void OCSiteColumn_Del(int ColumnID) {
            OCDAL.OCSiteColumn_Del(ColumnID);
            
        }
       /// <summary>
        /// 获取网站的栏目下子栏目列表
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
        public List<OCSiteColumn> OCSiteColumn_List(int ColumnID, int UserID) {
            return OCDAL.OCSiteColumn_List(ColumnID,UserID);
        }
       /// <summary>
        /// 更新栏目
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <param name="Title"></param>
       /// <param name="ContentType"></param>
        public void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType)
        {
            OCDAL.OCSiteColumn_Upd(ColumnID, Title, ContentType);
        }

       /// <summary>
        /// 更新父栏目
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <param name="ParentID"></param>
        public void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID)
        {
            OCDAL.OCSiteColumn_ParentID_Upd(ColumnID,ParentID);
        }
       /// <summary>
        /// 更新网站栏目的顺序 (Direction: orderup ,  orderdown , levelup , leveldown)
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <param name="Direction"></param>
        public void OCSiteColumn_Move(int ColumnID, string Direction) {
            OCDAL.OCSiteColumn_Move(ColumnID,Direction);
        }
       /// <summary>
        /// 获取网站栏目详细列表
       /// </summary>
       /// <param name="ColumnID"></param>
       /// <returns></returns>
        public List<OCSiteColumn> OCSiteColumn_Get(int ColumnID) {
            return OCDAL.OCSiteColumn_Get(ColumnID);
        }
       /// <summary>
        /// 获取在线课程的基本信息
       /// </summary>
       /// <param name="OCID"></param>
       /// <returns></returns>
        public List<IES.CC.OC.Model.OC> OC_Get(int OCID) {
            return OCDAL.OC_Get(OCID);
        }
       /// <summary>
        /// 获取课程通知列表
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="UserID"></param>
       /// <param name="PageIndex"></param>
       /// <param name="PageSize"></param>
       /// <returns></returns>
        public List<OCNotice> OCNotice_List(int OCID, int UserID, int PageIndex, int PageSize) {
            return OCDAL.OCNotice_List(OCID, UserID, PageIndex, PageSize);
        }
       /// <summary>
        /// 获取网站下视频的预览
       /// </summary>
       /// <param name="OCID"></param>
       /// <returns></returns>
        public List<IES.Resource.Model.File> File_OCPreviewMP4_List(int OCID) {
            return OCDAL.File_OCPreviewMP4_List(OCID);
        }
        
    }
}
