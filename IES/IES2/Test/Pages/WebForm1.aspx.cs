using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using IES.Security;



namespace Test.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /// Label1.Text = IESCookie.GetCookieValue("ies"); 

            //从缓存中获取所有的对象信息
            List<IES.CC.OC.Model.OC> oclist = IES.G2S.OC.DAL.OCDAL.OC_Cache_List();
            List<IES.CC.OC.Model.OCTeam> teamlist = IES.G2S.OC.DAL.Team.OCTeamDAL.OCTeam_Cache_List();
            List<IES.JW.Model.User> userlist = IES.G2S.JW.DAL.UserDAL.User_Cache_List();
            List<IES.CC.Model.OC.OCTemplate> templatelist = IES.G2S.OC.DAL.OCTemplateDAL.OCTemplate_List(); 



            int curocid = 123; //在线OCID
            int courseid = 1; //课程的ID

            List<IES.CC.OC.Model.OC> oclist1 = new List<IES.CC.OC.Model.OC>();   

            var query = from oc in oclist
                    from team in teamlist
                    from user in userlist
                    from template in templatelist 
                    where
                    (
                        oc.OCID == team.OCID &&  user.UserID == team.UserID &&
                        oc.OCID != curocid && oc.CourseID == courseid  &&
                        oc.TemplateID == template.TemplateID 
                    )
                    select new { oc.CourseID,oc.OCID,user.UserName,template.URL, oc.TemplateID  } ;

            foreach (var item in query)
            {
                oclist1.Add(new IES.CC.OC.Model.OC { OCID =item.OCID, CourseID = item.CourseID, TemplateID = item.TemplateID, URL = item.URL, UserName = item.UserName });
            }

            



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             List<IES.Resource.Model.Folder > folderlist = new List<IES.Resource.Model.Folder>();
             folderlist.Add(new IES.Resource.Model.Folder{ FolderID=1 } );
              folderlist.Add(new IES.Resource.Model.Folder{ FolderID=2 } );
              IES.G2S.Resource.DAL.FileDAL.Folder_Batch_Del(folderlist);
        }
    }
}