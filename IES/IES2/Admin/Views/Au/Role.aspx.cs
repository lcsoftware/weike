using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Au
{
    public partial class Role : System.Web.UI.Page
    {
        private static int SysID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();
            TreeData();
            if (!IsPostBack)
            {
                if (Session["Role"] == null)
                    Session["Role"] = "1,2";
                AspNetPager1.PageSize = 20;
                DataBinder(1);
            }
        }
        #region 绑定数据
        public void DataBinder(int pageindex)
        {
            string roleitem = this.hfID.Value;
            string roleqg = this.hfQG.Value;
            Session["Role"] = roleitem + "," + roleqg;
            RoleSysList();
            RoleUserList(pageindex);
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            DataBinder(AspNetPager1.CurrentPageIndex);
        }
        //权限树
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Repeater rep = e.Item.FindControl("Repeater2") as Repeater;//找到里层的repeater对象
                //IES.JW.Model.Sys rowv = e.Item.DataItem as IES.JW.Model.Sys;
                //TreeView Treeview1 = e.Item.FindControl("TreeView1") as TreeView; ;  //找到分类Repeater关联的数据项 
                //int sid = rowv.sysid; //获取填充子类的id 
                //Treeview1.Nodes.Clear();
                //LoadTree(Treeview1.Nodes, "0",sid);
                //Treeview1.Attributes.Add("onclick", "OnTreeNodeChecked()");
            }
        }
        //生成树
        public string TreeData()
        {
            var datas = "";
            List<IES.JW.Model.AuModule> arllist = arlTree();
            for (var i = 0; i < arllist.Count; i++)
            {
                if (i == arllist.Count - 1)
                {
                    var menudata = " { id: '" + arllist[i].ModuleID + "', pId: '" + arllist[i].ParentID + "', name: '" + arllist[i].Name + "', open:true }";
                    datas += menudata;
                }
                else
                {
                    var menudata = " { id: '" + arllist[i].ModuleID + "', pId: '" + arllist[i].ParentID + "', name: '" + arllist[i].Name + "', open:true},";
                    datas += menudata;
                }
            }
            return datas;
        }
        public List<IES.JW.Model.AuModule> arlTree()
        {
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { SysID = SysID };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.AuModule> aurollist = aurolbll.AuModelAction_Tree(_aurol);
            return aurollist;
        }
        private void LoadTree(TreeNodeCollection nodes, string parentId,int sid)
        {
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { SysID = sid };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.AuModule> aurollist = aurolbll.AuModelAction_Tree(_aurol);
            var listAdminMenu = aurollist.Where(t => t.ParentID == parentId);
            if (listAdminMenu.Count() == 0) return;
            foreach (var menu in listAdminMenu)
            {
                var tNode = new TreeNode(menu.Name, menu.ModuleID.ToString());
                nodes.Add(tNode);
                LoadTree(tNode.ChildNodes, menu.ModuleID,sid);
            }
        }
        //绑定角色
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            Repeater1.DataSource = parmlist.arolist;
            Repeater1.DataBind();

            int id = Convert.ToInt32(this.hfID.Value);
            for (var i = 0; i < parmlist.syslist.Count; i++)
            {
                if (parmlist.syslist[i].RoleID == id)
                {
                    this.SysName.InnerText = parmlist.syslist[i].name;
                    SysID = parmlist.syslist[i].sysid;
                }
            }          
            
            for(var i=0;i<parmlist.arolist.Count;i++)
            {
                if(parmlist.arolist[i].RoleID==id)
                {
                    this.RoleName.InnerText = parmlist.arolist[i].Title;
                }
            }
        }
        //保存Session
        public string SaveSession()
        {
            string roleitem = this.hfID.Value;
            string roleqg = this.hfQG.Value;
            Session["Role"] = roleitem + "," + roleqg;
            return null;
        }
        //得到Session添加样式
        public string Getclass()
        {
            string parms = Session["Role"].ToString();
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "../Portal/Edit.js", "<script>Roleclass('" + parms + "');</script>");
            return null;
        }
        #endregion

        #region 操作
        //删除角色
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { RoleID = id };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            bool result = aurolbll.AuRole_Del(_aurol);
            if (result == true)
            {
                this.hfID.Value = "1";
                DataParm();
                DataBinder(1);              
            }
        }
        //新增角色
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string title = this.newrol.Value;
            if (title == "")
            {Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请输入新角色！');</script>");}
            else
            {
                IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { Title = title };
                IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
                IES.JW.Model.AuRole result = aurolbll.AuRole_ADD(_aurol);
                if (result != null)
                {
                    DataParm();
                    DataBinder(1);
                }
            }
        }
        //角色重命名
        protected void btnUpd_Click(object sender, EventArgs e)
        {
            int id=Convert.ToInt32(this.hfID.Value);
            string newname = Request["txt" + id + ""].ToString();
            if (newname == "")
            { Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('请输入新角色！');</script>"); }
            else
            {
                IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole {RoleID=id, Title = newname };
                IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
                bool result = aurolbll.AuRole_Upd(_aurol);
                if (result != false)
                {
                    DataParm();
                    DataBinder(1);
                }
            }
        }
        //角色下平台列表
        protected void RoleSysList_Click(object sender, EventArgs e)
        {
            RoleSysList();
        }
        public void RoleSysList()
        {
            //int id = Convert.ToInt32(this.hfID.Value);
            //IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { RoleID = id };
            //IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            //List<IES.JW.Model.Sys> list = aurolbll.AuRoleSys_List(_aurol);
            //Repeater2.DataSource = list;
            //Repeater2.DataBind();

        }
        //角色下用户列表
        protected void RoleUserList_Click(object sender, EventArgs e)
        {
            string roleitem = this.hfID.Value;
            string roleqg = this.hfQG.Value;
            Session["Role"] = roleitem + "," + roleqg;
            RoleUserList(1);
        }
        public void RoleUserList(int pageindex)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            string key = this.txtKey.Value;
            IES.JW.Model.User _aurol = new IES.JW.Model.User { Role = id, Key = key };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.User> list = aurolbll.AuUserRoleOrg_List(_aurol, pageindex, 20);
            Repeater3.DataSource = list;
            Repeater3.DataBind();
        }
        //切换角色更新数据
        protected void RoleChange_Click(object sender, EventArgs e)
        {
            string roleitem = this.hfID.Value;
            string roleqg = this.hfQG.Value;
            Session["Role"] = roleitem + "," + roleqg;
            DataBinder(1);
        }
        //新增角色下用户
        protected void BatchADD_Click(object sender, EventArgs e)
        {
            AddBatch();
        }
        public void AddBatch()
        {
            int rolid = Convert.ToInt32(this.hfID.Value);
            string IDS = this.hfIDS.Value;
            IES.JW.Model.User _user = new IES.JW.Model.User { Role = rolid, UserIDS = IDS };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            bool result = aurolbll.AuRoleUser_ADD(_user);
                DataBinder(1);
        }
        //删除角色下用户
        protected void BatchDel_Click(object sender, EventArgs e)
        {
            DelBatch();
        }
        public void DelBatch()
        {
            int rolid=Convert.ToInt32(this.hfID.Value);
            string IDS = this.hfIDS.Value;
            IES.JW.Model.User _user = new IES.JW.Model.User { Role = rolid, UserIDS = IDS };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            bool result = aurolbll.AuRoleUser_Del(_user);
            if (result == true)
            {
                DataBinder(1);
            }
        }
        //跳转修改权限页面
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            Response.Redirect("RoleEdit.aspx?id="+id, true);
        }
        #endregion

    }
}