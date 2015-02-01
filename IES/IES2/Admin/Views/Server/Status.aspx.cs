using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Server
{
    public partial class Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        #region 绑定数据
        private void DataBinder()
        {
            IES.G2S.JW.BLL.ResourceServerBLL serverbll = new IES.G2S.JW.BLL.ResourceServerBLL();
            List<IES.JW.Model.ResourceServer> serverlist = serverbll.ResourceServer_List();
            if (serverlist != null)
            {
                Repeater1.DataSource = serverlist;
                Repeater1.DataBind();
            }
        }
        #endregion

        #region 操作
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            IES.JW.Model.ResourceServer server = new IES.JW.Model.ResourceServer { ServerID=id};
            IES.G2S.JW.BLL.ResourceServerBLL serverbll = new IES.G2S.JW.BLL.ResourceServerBLL();
            bool result = serverbll.ResourceServer_Del(server);
            if (result == true)
            { DataBinder(); }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string host = this.host1.Value;
            string iisfolder = this.iisfolder1.Value;
            string iispost = this.iispost1.Value;
            string mmsfolder = this.mmsfolder1.Value;
            string mmsport = this.mmsport1.Value;
            string nginxfolder = this.nginxf1.Value;
            string nginxport = this.nginxp1.Value;
            string pubkey = this.pubkey1.Value;

            string brief = "品牌:" + pinpai1.Value + ";型号:" + xinhao1.Value + ";处理器:" + chuliqi1.Value + ";内存:" + neicun1.Value + ";硬盘:" +yinpan1.Value+ ";操作系统:" + xitong1.Value;

            IES.JW.Model.ResourceServer server = new IES.JW.Model.ResourceServer 
            { Host=host,IISFolder=iisfolder,IISPort=iispost,MMSFolder=mmsfolder,MMSPort=mmsport,NginxFolder=nginxfolder,NginxPort=nginxport,PubKey=pubkey,Brief=brief };
            IES.G2S.JW.BLL.ResourceServerBLL serverbll = new IES.G2S.JW.BLL.ResourceServerBLL();
            IES.JW.Model.ResourceServer result = serverbll.ResourceServer_Edit(server);
            if (result.ServerID != 0)
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('新增成功');", true); 
                DataBinder();
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            string host = this.host2.Value;
            string iisfolder = this.iisfolder2.Value;
            string iispost = this.iispost2.Value;
            string mmsfolder = this.mmsfolder2.Value;
            string mmsport = this.mmsport2.Value;
            string nginxfolder = this.nginxf2.Value;
            string nginxport = this.nginxp2.Value;
            string pubkey = this.pubkey2.Value;

            string brief = "品牌:" + pinpai2.Value + ";型号:" + xinhao2.Value + ";处理器:" + chuliqi2.Value + ";内存:" + neicun2.Value + ";硬盘:" +yinpan2.Value+ ";操作系统:" + xitong2.Value;

            IES.JW.Model.ResourceServer server = new IES.JW.Model.ResourceServer {ServerID=id, Host = host, IISFolder = iisfolder, IISPort = iispost, MMSFolder = mmsfolder, MMSPort = mmsfolder, NginxFolder = nginxfolder, NginxPort = nginxport, PubKey = pubkey, Brief = brief };
            IES.G2S.JW.BLL.ResourceServerBLL serverbll = new IES.G2S.JW.BLL.ResourceServerBLL();
            IES.JW.Model.ResourceServer result = serverbll.ResourceServer_Edit(server);
            if (result.ServerID != 0)
            {
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('修改成功');", true);
                DataBinder();
            }
        }
        
        #endregion

    }
}