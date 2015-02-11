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
            
            //FROM 1 资料  2 附件
            var from = context.Request.QueryString["FROM"];
            if (from.Equals("2"))
            {
                List<IES.Resource.Model.Attachment> attachMentList = IES.Service.FileService.AttachmentUpload();
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(attachMentList));
            }
            else
            { 
                IES.Resource.Model.File fileModel = new IES.Resource.Model.File();
                fileModel.OCID = int.Parse(context.Request.Form["OCID"]);
                fileModel.CourseID = int.Parse(context.Request.Form["CourseID"]);
                fileModel.FolderID = int.Parse(context.Request.Form["FolderID"] == "undefined" ? "0" : context.Request.Form["FolderID"]);
                fileModel.ShareRange = int.Parse(context.Request.Form["ShareRange"]); 
                List<IES.Resource.Model.File> fileList = IES.Service.FileService.ResourceFileUpload(fileModel);
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(fileList));
            } 
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