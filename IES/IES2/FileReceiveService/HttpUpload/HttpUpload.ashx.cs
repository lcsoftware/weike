using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FileReceiveService.HttpUpload
{
    /// <summary>
    /// HttpUpload 的摘要说明
    /// </summary>
    public class HttpUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            string uName = context.Request["UName"];
            string pwd = context.Request["PWD"];
            string dir = context.Server.MapPath(string.Format("~/"));
            string fileName = dir + "/" + context.Request["fileName"];
            if (fileName == string.Empty)
                return;
            try
            {

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                byte[] buff = new byte[40960];
                int len = 0;
                using (Stream inputStream = context.Request.InputStream)
                {
                    using (FileStream fileStream = File.Create(fileName))
                    {
                        while ((len = inputStream.Read(buff, 0, buff.Length)) > 0)
                        {
                            fileStream.Write(buff, 0, len);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try { File.Delete(fileName); }
                catch { }
                throw ex;
            }
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