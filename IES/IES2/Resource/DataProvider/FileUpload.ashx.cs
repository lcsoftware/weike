using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.G2S.DataProvider
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// 公用上传文件处理程序
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Cache-Control", "no-cache,must-revalidate");
            List<IES.Resource.Model.Attachment> list = IES.Service.FileService.AttachmentUpload();
            //IES.Service.FileService.ResourceFileUpload();
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(list));
            context.Response.End();
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