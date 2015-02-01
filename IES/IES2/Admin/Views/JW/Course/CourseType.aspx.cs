using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.JW.Course
{
    public partial class CourseType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void LoadTree(TreeNodeCollection nodes, int parentId)
        {
            IES.G2S.JW.BLL.CourseTypeBLL coursebll = new IES.G2S.JW.BLL.CourseTypeBLL();
            List<IES.JW.Model.Coursetype> courselist = coursebll.CourseType_Tree();
            var listAdminMenu = courselist.Where(t => t.ParentID == parentId);
            if (listAdminMenu.Count() == 0) return;
            foreach (var menu in listAdminMenu)
            {
                var tNode = new TreeNode(menu.Name, menu.CourseTypeID.ToString());
                nodes.Add(tNode);
                LoadTree(tNode.ChildNodes, menu.CourseTypeID);
            }
        }
    }
}