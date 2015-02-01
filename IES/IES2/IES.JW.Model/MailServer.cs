using System;

namespace IES.JW.Model
{
    [Serializable]
    public partial class MailServer
    {
        public MailServer()
        { }
        #region 补充信息
        public int op_MailID { get; set; }
        #endregion
        #region Model
        private int _ServerID;
        private string _SMTPServer;
        private int _Port;
        private string _Account;
        private string _Password;
        private bool _IsSSL;

        /// <summary>
        /// 
        /// </summary>
        public int ServerID
        {
            set { _ServerID = value; }
            get { return _ServerID; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SMTPServer
        {
            set { _SMTPServer = value; }
            get { return _SMTPServer; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            set { _Port = value; }
            get { return _Port; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Account
        {
            set { _Account = value; }
            get { return _Account; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _Password = value; }
            get { return _Password; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSSL
        {
            set { _IsSSL = value; }
            get { return _IsSSL; }
        }

        #endregion

    }
}
