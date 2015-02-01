using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.OC.IBLL.Site
{
    public interface ISiteBLL
    {

        System.Collections.Generic.List<IES.Resource.Model.File> File_OCPreviewMP4_List(int OCID);
        System.Collections.Generic.List<IES.CC.OC.Model.OC> OC_Cache_List();
        System.Collections.Generic.List<IES.CC.OC.Model.OC> OC_List(int userid, int role);
        System.Collections.Generic.List<IES.CC.OC.Model.OCNotice> OCNotice_List(int OCID, int UserID, int PageIndex, int PageSize);
        void OCSite_BuildMode_Upd(int OCID, int BuildMode, string OutSiteLink);
        bool OCSite_DisplayStyle_Upd(int SiteID, int DisplayStyle);
        void OCSite_Field_Upd(int OCID, int  ContentType);
        System.Collections.Generic.List<IES.CC.OC.Model.OCSite> OCSite_Get(int OCID, int UserID);
      
        void OCSite_Language_Upd(int SiteID, int Language);
        void OCSiteColumn_Conten_Upd(int ColumnID, string Conten);
        void OCSiteColumn_Del(int ColumnID);
        int OCSiteColumn_Edit(IES.CC.OC.Model.OCSiteColumn column);
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_Get(int ColumnID);
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_List(int ColumnID, int UserID);
        void OCSiteColumn_Move(int ColumnID, string Direction);
        void OCSiteColumn_ParentID_Upd(int ColumnID, int ParentID);
        System.Collections.Generic.List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_Tree(int OCID, int UserID);
        void OCSiteColumn_Upd(int ColumnID, string Title, int ContentType);
        System.Collections.Generic.List<IES.CC.OC.Model.OC> OC_Get(int ColumnID);

        void OC_Brief_Upd(int OCID, string Brief);

        List<IES.CC.OC.Model.OCSiteColumn> OCSiteColumn_Nav_Tree(int ColumnID, int OCID);
    }
}
