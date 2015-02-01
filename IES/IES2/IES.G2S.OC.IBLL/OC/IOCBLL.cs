using IES.CC.OC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.IBLL.OC
{
    
    public interface IOCBLL
    
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OC> OC_List(int userid, int role);
        /// <summary>
        /// 更新课程网站的建设模式
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="BuildMode"></param>
        /// <param name="OutSiteLink"></param>
        void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink);
        /// <summary>
        /// 网站显示风格更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="DisplayStyle"></param>
        /// <returns></returns>
        bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle);
        /// <summary>
        /// 网站栏目的启用
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="FileldType"></param>
        void OCSite_Field_Upd(int OCID, string FileldType);
        /// <summary>
        /// 获取网站的栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OCSite> OCSite_Get(int OCID, int UserID);
        /// <summary>
        /// 网站显示语言更新
        /// </summary>
        /// <param name="SiteID"></param>
        /// <param name="Language"></param>
        void OCSite_Language_Upd(int SiteID, int Language);
        /// <summary>
        /// 添加栏目
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        int OCSiteColumn_Edit(IES.CC.OC.Model.OCSiteColumn column);
        /// <summary>
        /// 网站栏目内容更新
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Conten"></param>
        void OCSiteColumn_Conten_Upd(int ColumnID, string Conten);
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        void OCSiteColumn_Del(int ColumnID);
        /// <summary>
        /// 获取网站的栏目下子栏目列表
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_List(int ColumnID, int UserID);
        /// <summary>
        /// 更新父栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="ParentID"></param>
        void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID);
        /// <summary>
        /// 获取网站的所有栏目列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_Tree(int OCID, int UserID);
        /// <summary>
        /// 更新栏目
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Title"></param>
        /// <param name="ContentType"></param>
        void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType);
        /// <summary>
        ///  更新网站栏目的顺序 (Direction: orderup ,  orderdown , levelup , leveldown)
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <param name="Direction"></param>
        void OCSiteColumn_Move(int ColumnID, string Direction);
        /// <summary>
        /// 获取网站栏目详细列表
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_Get(int ColumnID);
        /// <summary>
        /// 获取在线课程的基本信息
        /// </summary>
        /// <param name="ColumnID"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.CC.OC.Model.OC> OC_Get(int ColumnID);
        /// <summary>
        /// 获取课程通知列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <param name="UserID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        System.Collections.Generic.List<OCNotice> OCNotice_List(int OCID, int UserID, int PageIndex, int PageSize);
        /// <summary>
        /// 获取网站下视频的预览
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        System.Collections.Generic.List<IES.Resource.Model.File> File_OCPreviewMP4_List(int OCID);
    }
}
