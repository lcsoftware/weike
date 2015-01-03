using IES.CC.OC.Model;
using IES.G2S.OC.BLL.OC;
using IES.G2S.OC.IBLL.OC;
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
        public static int OCSiteColumn_ADD(string columnsname,int type)
        {
            OCSiteColumn column = new OCSiteColumn();
            column.ColumnID = -1;
            column.OCID = 1;
            column.Title = columnsname;
            column.UserID=1;
            column.ParentID = 0;
            column.ContentType = type;
            IOCBLL ocbll = new OCBLL();
           return ocbll.OCSiteColumn_ADD(column);
        }
        /// <summary>
        /// 获取网站列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<IES.CC.OC.Model.OC> OC_List() {
            string userid = IESCookie.GetCookieValue("ies");
            IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            IOCBLL ocbll = new OCBLL();
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
            IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            IOCBLL ocbll = new OCBLL();
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
            IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            IOCBLL ocbll = new OCBLL();
            OCSiteColumn ocsitecolumn=new OCSiteColumn ();
            List<OCSiteColumn>  listcolumn= ocbll.OCSiteColumn_Tree(OCID,user.UserID);
            ForeachPropertyNode(listcolumn, ocsitecolumn,0);
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
            IOCBLL ocbll = new OCBLL();
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
            IOCBLL ocbll = new OCBLL();
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
            IOCBLL ocbll = new OCBLL();
            ocbll.OCSite_BuildMode_Upd(OCID, BuildMode, OutSiteLink);
        }
        /// <summary>
        /// 网站栏目的启用
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="FileldType"></param>
        [WebMethod]
        public static void OCSite_Field_Upd(int OCID, string FileldType)
        {
            IOCBLL ocbll = new OCBLL();
            ocbll.OCSite_Field_Upd(OCID,FileldType);
        }
        /// <summary>
        /// 网站栏目内容更新
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Conten"></param>
        [WebMethod]
        public static void OCSiteColumn_Conten_Upd(int ColumnID, string Conten)
        {
            IOCBLL ocbll = new OCBLL();
            ocbll.OCSiteColumn_Conten_Upd(ColumnID, Conten);
        }
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        [WebMethod]
        public static void OCSiteColumn_Del(int ColumnID)
        {
            IOCBLL ocbll = new OCBLL();
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
            IES.SYS.Model.User user = new IES.SYS.Model.User { UserID = Int32.Parse(userid) };
            user = UserService.User_Get(user);
            IOCBLL ocbll = new OCBLL();
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
            IOCBLL ocbll = new OCBLL();
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
            IOCBLL ocbll = new OCBLL();
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
            IOCBLL ocbll = new OCBLL();
            ocbll.OCSiteColumn_Move(ColumnID,Direction);
        }


        private static void ForeachPropertyNode(List<OCSiteColumn> ocsitecloumn, OCSiteColumn node, int pid)
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
                        ContentType =view.ContentType
                    };
                    ForeachPropertyNode(ocsitecloumn, childNodeItem, childNodeItem.ColumnID);
                    node.Children.Add(childNodeItem);
                }
            }
        }

    }
}