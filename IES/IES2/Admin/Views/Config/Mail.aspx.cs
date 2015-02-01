using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin.Views.Config
{
    public partial class Mail : System.Web.UI.Page
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
            IES.G2S.JW.BLL.MailServerBLL mailbll = new IES.G2S.JW.BLL.MailServerBLL();
            List<IES.JW.Model.MailServer> maillist = mailbll.MailServer_List();
            for (var i = 0; i < maillist.Count; i++)
            {
                if (i == 0)
                {
                    this.Emailfwq0.Value = maillist[i].SMTPServer;
                    this.Port0.Value = maillist[i].Port.ToString();
                    this.Account0.Value = maillist[i].Account;
                    this.Password0.Value = maillist[i].Password;
                    this.sslJM0.Checked = maillist[i].IsSSL;
                }
                if (i == 1)
                {
                    this.Emailfwq1.Value = maillist[i].SMTPServer;
                    this.Port1.Value = maillist[i].Port.ToString();
                    this.Account1.Value = maillist[i].Account;
                    this.Password1.Value = maillist[i].Password;
                    this.sslJM1.Checked = maillist[i].IsSSL;
                }
                if (i == 2)
                {
                    this.Emailfwq2.Value = maillist[i].SMTPServer;
                    this.Port2.Value = maillist[i].Port.ToString();
                    this.Account2.Value = maillist[i].Account;
                    this.Password2.Value = maillist[i].Password;
                    this.sslJM2.Checked = maillist[i].IsSSL;
                }
                if (i == 3)
                {
                    this.Emailfwq3.Value = maillist[i].SMTPServer;
                    this.Port3.Value = maillist[i].Port.ToString();
                    this.Account3.Value = maillist[i].Account;
                    this.Password3.Value = maillist[i].Password;
                    this.sslJM3.Checked = maillist[i].IsSSL;
                }
                if (i == 4)
                {
                    this.Emailfwq4.Value = maillist[i].SMTPServer;
                    this.Port4.Value = maillist[i].Port.ToString();
                    this.Account4.Value = maillist[i].Account;
                    this.Password4.Value = maillist[i].Password;
                    this.sslJM4.Checked = maillist[i].IsSSL;
                }
                if (i == 5)
                {
                    this.Emailfwq5.Value = maillist[i].SMTPServer;
                    this.Port5.Value = maillist[i].Port.ToString();
                    this.Account5.Value = maillist[i].Account;
                    this.Password5.Value = maillist[i].Password;
                    this.sslJM5.Checked = maillist[i].IsSSL;
                }
            }

        }
        //清空数据
        private void ClearData()
        {
            this.Emailfwq0.Value = "";
            this.Port0.Value = "";
            this.Account0.Value = "";
            this.Password0.Value = "";
            this.sslJM0.Checked = false;
            this.Emailfwq1.Value = "";
            this.Port1.Value = "";
            this.Account1.Value = "";
            this.Password1.Value = "";
            this.sslJM1.Checked = false;
            this.Emailfwq2.Value = "";
            this.Port2.Value = "";
            this.Account2.Value = "";
            this.Password2.Value = "";
            this.sslJM2.Checked = false;
            this.Emailfwq3.Value = "";
            this.Port3.Value = "";
            this.Account3.Value = "";
            this.Password3.Value = "";
            this.sslJM3.Checked = false;
            this.Emailfwq4.Value = "";
            this.Port4.Value = "";
            this.Account4.Value = "";
            this.Password4.Value = "";
            this.sslJM4.Checked = false;
            this.Emailfwq5.Value = "";
            this.Port5.Value = "";
            this.Account5.Value = "";
            this.Password5.Value = "";
            this.sslJM5.Checked = false;
        }
        #endregion

        #region 操作
        //新增或修改
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(this.hfID.Value);
            EditEmail(id);
            ClearData();
            DataBinder();
        }
        private void EditEmail(int i)
        {
            string mailfwq = "";
            int port = 0;
            string account = "";
            string pwd = "";
            bool ssl = false;
            if (i == 0)
            {
                mailfwq = this.Emailfwq0.Value;
                port = int.Parse(this.Port0.Value);
                account = this.Account0.Value;
                pwd = this.Password0.Value;
                ssl = this.sslJM0.Checked;
            }
            if (i == 1)
            {
                mailfwq = this.Emailfwq1.Value;
                port = int.Parse(this.Port1.Value);
                account = this.Account1.Value;
                pwd = this.Password1.Value;
                ssl = this.sslJM1.Checked;
            }
            if (i == 2)
            {
                mailfwq = this.Emailfwq2.Value;
                port = int.Parse(this.Port2.Value);
                account = this.Account2.Value;
                pwd = this.Password2.Value;
                ssl = this.sslJM2.Checked;
            }
            if (i == 3)
            {
                mailfwq = this.Emailfwq3.Value;
                port = int.Parse(this.Port3.Value);
                account = this.Account3.Value;
                pwd = this.Password3.Value;
                ssl = this.sslJM3.Checked;
            }
            if (i == 4)
            {
                mailfwq = this.Emailfwq4.Value;
                port = int.Parse(this.Port4.Value);
                account = this.Account4.Value;
                pwd = this.Password4.Value;
                ssl = this.sslJM4.Checked;
            }
            if (i == 5)
            {
                mailfwq = this.Emailfwq5.Value;
                port = int.Parse(this.Port5.Value);
                account = this.Account5.Value;
                pwd = this.Password5.Value;
                ssl = this.sslJM5.Checked;
            }
            IES.G2S.JW.BLL.MailServerBLL mailbll = new IES.G2S.JW.BLL.MailServerBLL();
            List<IES.JW.Model.MailServer> maillist = mailbll.MailServer_List();
            int id = 0;
            if (maillist.Count > i)
            {
                id = maillist[i].ServerID;
            }

            IES.JW.Model.MailServer mail = new IES.JW.Model.MailServer { ServerID = id, SMTPServer = mailfwq, Port = port, Account = account, Password = pwd, IsSSL = ssl };
            IES.JW.Model.MailServer result = mailbll.MailServer_Edit(mail);
            ClearData();
            DataBind();
        }
        //删除
        protected void btnInfo_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(this.hfID.Value);
            IES.G2S.JW.BLL.MailServerBLL mailbll = new IES.G2S.JW.BLL.MailServerBLL();
            List<IES.JW.Model.MailServer> maillist = mailbll.MailServer_List();
            int id = 0;
            if (maillist.Count != 0)
            {
                id = maillist[i].ServerID;
            }
            IES.JW.Model.MailServer mail = new IES.JW.Model.MailServer { ServerID = id };
            bool result = mailbll.MailServer_Del(mail);
            ClearData();
            DataBinder();
        }

        #endregion
    }
}