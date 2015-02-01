using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Au
{
    public partial class RoleEdit : System.Web.UI.Page
    {
        private static int sysid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DataParm();
        }
        public void DataParm()
        {
            IES.G2S.JW.BLL.ParmInfoBLL parmbll = new IES.G2S.JW.BLL.ParmInfoBLL();
            IES.JW.Model.ParmInfo parmlist = parmbll.Parm_Info_List();

            int id = Convert.ToInt32(Request["id"]);
            for (var i = 0; i < parmlist.arolist.Count; i++)
            {
                if (parmlist.arolist[i].RoleID == id)
                {
                    sysid = Convert.ToInt32(parmlist.arolist[i].SysID);
                }
            }
            for (var i = 0; i < parmlist.syslist.Count; i++)
            {
                if (parmlist.syslist[i].sysid == sysid)
                {
                    this.SysName.InnerText = parmlist.syslist[i].name;
                }
            }
        }
        //绑定树
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
            int sysid = Convert.ToInt32(Request["id"]);
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { SysID = sysid };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.AuModule> aurollist = aurolbll.AuModelAction_Tree(_aurol);
            return aurollist;
        }
        //子系统下角色拥有的权限
        public string AuRoleModule_List()
        {
            string RolMod = "";
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { RoleID = 1 };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.AuRoleModule> aurolmodlist = aurolbll.AuRoleModule_List(_aurol);
            for (var i = 0; i < aurolmodlist.Count; i++)
            {
                if (i == aurolmodlist.Count - 1)
                {
                    if (aurolmodlist[i].ActionID==null)
                    {
                        RolMod += aurolmodlist[i].ModuleID;
                    }
                    else
                    { RolMod += aurolmodlist[i].ActionID; }
                }
                else
                {
                    if (aurolmodlist[i].ActionID == null)
                    {
                        RolMod += aurolmodlist[i].ModuleID + ",";
                    }
                    else
                    { RolMod += aurolmodlist[i].ActionID + ","; }
                }
            }
            return RolMod;
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ids = this.hfIDS.Value;

            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();

            var ary = ids.Split(',');
            List<IES.JW.Model.AuAction> actlist = aurolbll.AuAction_List();
            List<IES.JW.Model.AuAction> newact = new List<IES.JW.Model.AuAction>();
            for (var i = 0; i < ary.Length; i++)
            {
                IES.JW.Model.AuAction m = new IES.JW.Model.AuAction();
                m.ModuleID = ary[i];
                newact.Add(m);
            }
            foreach (var act in actlist)
            {
                IES.JW.Model.AuAction m = new IES.JW.Model.AuAction();
                for (var i = 0; i < newact.Count; i++)
                {
                    if (act.ActionID == newact[i].ModuleID)
                    {
                        m.ActionID = act.ActionID;
                        m.ModuleID = act.ModuleID;
                        newact[i] = m;
                    }
                }
            }
            string IDS = "";
            for (var i = 0; i < newact.Count; i++)
            {
                if (i == newact.Count - 1)
                {
                    IDS += newact[i].ModuleID + "@" + newact[i].ActionID;
                }
                else
                { 
                    IDS += newact[i].ModuleID + "@" + newact[i].ActionID + ";";
                }
            }
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { ModIDS = IDS };
            bool result = aurolbll.AuRoleModule_Edit(_aurol);
            if (result == true)
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('修改成功');", true); 
            }
        }
    }
}