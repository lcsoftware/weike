using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.G2S.Views.CourseLive.Forum
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            bool rs = false;
            List<IES.Resource.Model.Attachment> list = IES.Service.FileService.AttachmentUpload();
            for (int i = 0; i < list.Count; i++)
            {
                string guid = list[i].Guid;
                int sourceid = 1;
                string source = "ForumTopic";
                IES.Resource.Model.Attachment atmt = new IES.Resource.Model.Attachment { Guid = guid, Source = source, SourceID = sourceid };
                rs = IES.Service.FileService.AttachmentRelation(atmt);
            }
            if (rs)
            {
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(0);
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