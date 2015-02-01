using IES.CC.Model.OC;
using IES.CC.OC.Model;
using IES.G2S.OC.BLL.OC;
using IES.G2S.OC.BLL.Site;
using IES.G2S.OC.BLL.Team;
using IES.G2S.OC.IBLL.OC;
using IES.G2S.OC.IBLL.Site;
using IES.G2S.OC.IBLL.Team;
using IES.Security;
using IES.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.AngularMvc.DataProvider.OC.Site
{
    public partial class SiteProvider : System.Web.UI.Page
    {
        /// <summary>
        /// 新增主栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        [WebMethod]
        public static int OCSiteColumn_Edit(string columnsname, int type, int OCID, int ColumnID, int ParentID)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            OCSiteColumn column = new OCSiteColumn();
            column.ColumnID = ColumnID;
            column.OCID = OCID;
            column.Title = columnsname;
            column.UserID = user.UserID;
            column.ParentID = ParentID;
            column.ContentType = type;
            ISiteBLL ocbll = new SiteBLL();
           return ocbll.OCSiteColumn_Edit(column);
        }
        /// <summary>
        /// 获取网站列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.CC.OC.Model.OC> OC_List() {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OC_List(user.UserID, user.UserType);
        }
        /// <summary>
        /// 获取网站栏目
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCSite> OCSite_Get(int OCID) {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OCSite_Get(OCID,user.UserID);
        }
        /// <summary>
        /// 获取网站的所有栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCSiteColumn> OCSiteColumn_Tree(int OCID)
        {
             string userid = IESCookie.GetCookieValue("ies");
             IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ISiteBLL ocbll = new SiteBLL();
            OCSite ocsite= ocbll.OCSite_Get(OCID, user.UserID)[0];
            OCSiteColumn ocsitecolumn=new OCSiteColumn ();
            List<OCSiteColumn>  listcolumn= ocbll.OCSiteColumn_Tree(OCID,user.UserID);
            ForeachPropertyNode(listcolumn, ocsitecolumn, 0, ocsite);
            return ocsitecolumn.Children;
        }


        /// <summary>
        /// 网站显示风格更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="DisplayStyle"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle) {
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OCSite_DisplayStyle_Upd(SiteID, DisplayStyle);
        }
        /// <summary>
        /// 网站显示语言更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="Language"></param>
        [WebMethod]
        public static void OCSite_Language_Upd(int SiteID, int Language)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSite_Language_Upd(SiteID, Language);
        }
        /// <summary>
        /// 更新课程网站的建设模式
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="BuildMode"></param>
        /// <param name="OutSiteLink"></param>
        [WebMethod]
        public static void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSite_BuildMode_Upd(OCID, BuildMode, OutSiteLink);
        }
        /// <summary>
        /// 网站栏目的启用
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="FileldType"></param>
        [WebMethod]
        public static void OCSite_Field_Upd(int OCID, int ContentType)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSite_Field_Upd(OCID, ContentType);
        }
        /// <summary>
        /// 网站栏目内容更新
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Conten"></param>
        [WebMethod]
        public static void OCSiteColumn_Conten_Upd(int ColumnID, string Conten)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSiteColumn_Conten_Upd(ColumnID, Conten);
        }
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        [WebMethod]
        public static void OCSiteColumn_Del(int ColumnID)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSiteColumn_Del(ColumnID);
        }
        /// <summary>
        /// 获取网站的栏目下子栏目列表
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCSiteColumn> OCSiteColumn_List(int ColumnID)
        {
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OCSiteColumn_List(ColumnID, user.UserID);
        }
        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Title"></param>
        /// <param name="ContentType"></param>
        [WebMethod]
        public static void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSiteColumn_Upd(ColumnID,Title,ContentType);
        }
        /// <summary>
        /// 更新父栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="ParentID"></param>
        [WebMethod]
        public static void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSiteColumn_ParentID_Upd(ColumnID, ParentID);
        }
        /// <summary>
        /// 更新网站栏目的顺序 (Direction: orderup ,  orderdown , levelup , leveldown)
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Direction"></param>
        [WebMethod]
        public static void OCSiteColumn_Move(int ColumnID, string Direction)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OCSiteColumn_Move(ColumnID,Direction);
        }
        /// <summary>
        /// 获取网站栏目详细列表
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCSiteColumn> OCSiteColumn_Get(int ColumnID) {
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OCSiteColumn_Get(ColumnID);
        }
       /// <summary>
        /// 获取在线课程教学团队列表
       /// </summary>
       /// <param name="OCID"></param>
       /// <param name="Role"></param>
       /// <returns></returns>
        [WebMethod]
        
        public static List<OCTeam> OCTeam_List(int OCID,int Role) {
            OCTeamBLL octeambll = new OCTeamBLL();
            List<OCTeam> octeam= octeambll.OCTeam_List(OCID);
            return octeam.Where(x => x.Role == Role).ToList<OCTeam>();
            
        }
        /// <summary>s
        /// 获取模板列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<OCTemplate> OCTemplate_List() {
            IOCTemplateBLL octemplatebll = new OCTemplateBLL();
            return octemplatebll.OCTemplate_List();
        }
        /// <summary>
        /// 获取在线课程的基本信息
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.CC.OC.Model.OC> OC_Get(int OCID) {
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.OC_Get(OCID);
        }
        /// <summary>
        /// 获取课程通知列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<OCNotice> OCNotice_List(int OCID, int PageIndex, int PageSize) {

            ISiteBLL ocbll = new SiteBLL();
            string userid = IESCookie.GetCookieValue("ies");
            IES.JW.Model.User user = new IES.JW.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            return ocbll.OCNotice_List(OCID, user.UserID, PageIndex, PageSize);
        }
        /// <summary>
        /// 获取网站下视频的预览
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.Resource.Model.File> File_OCPreviewMP4_List(int OCID) {
            ISiteBLL ocbll = new SiteBLL();
            return ocbll.File_OCPreviewMP4_List(OCID);
        }
        /// <summary>
        /// 课程网站推荐词
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="Brief"></param>
        [WebMethod]
        public static void OC_Brief_Upd(int OCID, string Brief)
        {
            ISiteBLL ocbll = new SiteBLL();
            ocbll.OC_Brief_Upd(OCID, Brief);
        }

        [WebMethod]
        public static List<OCSiteColumn> OCSiteColumn_Nav_Tree(int ColumnID, int OCID)
        {
            ISiteBLL ocbll = new SiteBLL();
            List<OCSiteColumn> ocsitecolumn= ocbll.OCSiteColumn_Nav_Tree(ColumnID, OCID);
            ocsitecolumn.Reverse();
            return ocsitecolumn;
        }





        private static void ForeachPropertyNode(List<OCSiteColumn> ocsitecloumn, OCSiteColumn node, int pid, OCSite ocsite)
        {
            List<OCSiteColumn> dvDict = ocsitecloumn.FindAll(delegate(OCSiteColumn item) { return item.ParentID == pid; });
            if (dvDict.Count > 0)
            {
                foreach (OCSiteColumn view in dvDict)
                {
                    OCSiteColumn childNodeItem = new OCSiteColumn()
                    {
                        ColumnID =view.ColumnID,
                        OCID = view.OCID,
                        UserID =view.UserID,
                        ParentID = view.ParentID,
                        Title = view.Title,
                        Orde =view.Orde,
                        CreateTime =view.CreateTime,
                        Updatetime = view.Updatetime,
                        ContentType =view.ContentType,
                        HasChild=view.HasChild,
                        Conten=view.Conten,
                        IsShow=true
                       
                    };
                    if (pid == 0)
                    {
                        if (childNodeItem.ContentType == 11) {
                            if (ocsite.UseIndexPage)
                            {
                                childNodeItem.IsShow = true;
                            }
                            else {
                                childNodeItem.IsShow = false; 
                            }
                        }
                        else if (childNodeItem.ContentType == 13) {
                            if (ocsite.UseResource)
                            {
                                childNodeItem.IsShow = true;
                            }
                            else {
                                childNodeItem.IsShow = false;
                            }
                        }
                        else if (childNodeItem.ContentType == 14) {
                            if (ocsite.UseLive)
                            {
                                childNodeItem.IsShow = true;
                            }
                            else {
                                childNodeItem.IsShow = false;
                            }
                        }
                        else if (childNodeItem.ContentType == 12)
                        {

                            if (ocsite.UseMoocPlan)
                            {
                                childNodeItem.IsShow = true;
                            }
                            else {
                                childNodeItem.IsShow = false;
                            }
                        }
                        
                        childNodeItem.UseIndexPage = ocsite.UseIndexPage;
                        childNodeItem.UseResource = ocsite.UseResource;
                        childNodeItem.UseLive = ocsite.UseLive;
                        childNodeItem.UseMoocPlan = ocsite.UseMoocPlan;


                    }
                    ForeachPropertyNode(ocsitecloumn, childNodeItem, childNodeItem.ColumnID, ocsite);
                    node.Children.Add(childNodeItem);
                }
            }
        }

    }
}