using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Views.Au
{
    /// <summary>
    /// TreeView 的摘要说明
    /// </summary>
    public class TreeView : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (!string.IsNullOrEmpty(action))
            {
                try
                {
                    System.Reflection.MethodInfo method = this.GetType().GetMethod(action);
                    method.Invoke(this, new object[] { context });
                }
                catch (Exception ex)
                {
                    context.Response.Write("False");
                    context.Response.End();
                }
            }
        }

        //子系统下权限
        public void AuModule_List(HttpContext context)
        {

            int SysID = Convert.ToInt32(context.Request["SysID"]);
            IES.JW.Model.AuRole _aurol = new IES.JW.Model.AuRole { SysID = SysID };
            IES.G2S.JW.BLL.AuRoleBLL aurolbll = new IES.G2S.JW.BLL.AuRoleBLL();
            List<IES.JW.Model.AuModule> aurollist = aurolbll.AuModelAction_Tree(_aurol);
            string treedata = TreeData(aurollist);
            if (aurollist != null)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(treedata));
            }
            else
            {
                context.Response.Write("False");
            }
        }
        public string TreeData(List<IES.JW.Model.AuModule> arllist)
        {
            var datas = "";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}