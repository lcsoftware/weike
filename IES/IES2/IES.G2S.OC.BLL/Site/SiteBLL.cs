using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.Site;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL.Site;
using IES.Security;
using IES.Cache;

namespace IES.G2S.OC.BLL.Site
{
    public class SiteBLL : ISiteBLL
    {
        #region  列表
        public List<IES.CC.OC.Model.OC> OC_List(int userid, int role)
        {
            ICache cache = CacheFactory.Create();

            if (!cache.Exists(userid.ToString(), "OC_List"))
            {
                List<IES.CC.OC.Model.OC> oclist = SiteDAL.OC_List(userid, role);
                cache.Set(userid.ToString(), "OC_List", oclist);
                return oclist;
            }
            else
            {
                return cache.Get<List<IES.CC.OC.Model.OC>>(userid.ToString(), "OC_List");
            }
        }
        /// <summary>
        /// 获取所有的在线课程列表
        /// </summary>
        /// <returns></returns>
        public List<IES.CC.OC.Model.OC> OC_Cache_List() {

            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "OC_Cache_List"))
            {
                List<IES.CC.OC.Model.OC> octeamlist = SiteDAL.OC_Cache_List();
                cache.Set(string.Empty, "OC_Cache_List", octeamlist);
                return octeamlist;
            }
            else
            {
                return cache.Get<List<IES.CC.OC.Model.OC>>(string.Empty, "OC_Cache_List");
            }
        }
       
       

        /// <summary>
        /// 获取网站的所有栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<OCSiteColumn> OCSiteColumn_Tree(int OCID, int UserID) {
            List<OCSiteColumn> ocsiteTree = SiteDAL.OCSiteColumn_Tree(OCID, UserID).ToList();
            if (ocsiteTree != null)
            {
                return ocsiteTree.OrderBy(i => i.Orde).ToList();
            }
            else
            {
                return ocsiteTree;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public  List<OCSiteColumn> OCSiteColumn_List(int ColumnID, int UserID)
        {
            return SiteDAL.OCSiteColumn_List(ColumnID, UserID);
        }
        /// <summary>
        /// 获取课程通知列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public  List<OCNotice> OCNotice_List(int OCID, int UserID, int PageIndex, int PageSize)
        {
            return SiteDAL.OCNotice_List(OCID, UserID, PageIndex, PageSize);
        }

        /// <summary>
        /// 获取网站下视频的预览
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public  List<IES.Resource.Model.File> File_OCPreviewMP4_List(int OCID)
        {
            return SiteDAL.File_OCPreviewMP4_List(OCID);
        }

        public List<OCSiteColumn> OCSiteColumn_Nav_Tree(int ColumnID, int OCID) {
            return SiteDAL.OCSiteColumn_Nav_Tree(ColumnID, OCID);
        }

        #endregion

        #region  详细信息
        /// <summary>
        /// 获取网站的栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public  List<OCSite> OCSite_Get(int OCID, int UserID)
        {
            return SiteDAL.OCSite_Get(OCID, UserID);  
        }

        /// <summary>
        /// 获取网站栏目详细列表
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <returns></returns>
        public  List<OCSiteColumn> OCSiteColumn_Get(int ColumnID)
        {
            return SiteDAL.OCSiteColumn_Get(ColumnID);  
        }

        /// <summary>
        /// 获取在线课程的基本信息
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<IES.CC.OC.Model.OC> OC_Get(int OCID)
        {
            return SiteDAL.OC_Get(OCID);
        }
        #endregion

        #region  对象更新
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public  int OCSiteColumn_Edit(OCSiteColumn column)
        {
            return SiteDAL.OCSiteColumn_Edit(column);
        }
        /// <summary>
        /// 网站显示风格更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="DisplayStyle"></param>
        /// <returns></returns>
        public  bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle)
        {
            return SiteDAL.OCSite_DisplayStyle_Upd(SiteID, DisplayStyle);
        }
        /// <summary>
        /// 网站显示语言更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="Language"></param>
        public  void OCSite_Language_Upd(int SiteID, int Language)
        {
            SiteDAL.OCSite_Language_Upd(SiteID, Language);
        }
        /// <summary>
        /// 更新课程网站的建设模式
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="BuildMode"></param>
        /// <param name="OutSiteLink"></param>
        public  void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink)
        {
            SiteDAL.OCSite_BuildMode_Upd(OCID, BuildMode, OutSiteLink);
        }

        /// <summary>
        /// 网站栏目的启用
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="FileldType"></param>
        public void OCSite_Field_Upd(int OCID, int ContentType)
        {
            SiteDAL.OCSite_Field_Upd(OCID, ContentType);
        }

        /// <summary>
        /// 网站栏目内容更新
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Conten"></param>
        public  void OCSiteColumn_Conten_Upd(int ColumnID, string Conten)
        {
            SiteDAL.OCSiteColumn_Conten_Upd(ColumnID, Conten);
        }

        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Title"></param>
        /// <param name="ContentType"></param>
        public  void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType)
        {
            SiteDAL.OCSiteColumn_Upd(ColumnID, Title, ContentType);
        }

        /// <summary>
        /// 更新父栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="ParentID"></param>
        public  void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID)
        {
            SiteDAL.OCSiteColumn_ParentID_Upd(ColumnID, ParentID);
        }

        /// <summary>
        /// 更新网站栏目的顺序 (Direction: orderup ,  orderdown , levelup , leveldown)
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Direction"></param>
        public  void OCSiteColumn_Move(int ColumnID, string Direction)
        {
            SiteDAL.OCSiteColumn_Move(ColumnID, Direction);
        }
        /// <summary>
        /// 课程网站推荐词
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="Brief"></param>
        public void OC_Brief_Upd(int OCID, string Brief)
        {
            SiteDAL.OC_Brief_Upd(OCID, Brief);
        }

        #endregion

        #region  删除
        public  void OCSiteColumn_Del(int ColumnID)
        {
            SiteDAL.OCSiteColumn_Del(ColumnID);
        }
        #endregion
    }
}
