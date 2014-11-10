using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Score.DataProvider
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        private string _UploaderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");
        public void ProcessRequest(HttpContext context)
        {
            string newFileName = Guid.NewGuid().ToString().Replace("-", "");
            int index = context.Request.Files[0].FileName.LastIndexOf('.');
            if (index == -1) { index = 0; }
            newFileName += context.Request.Files[0].FileName.Substring(index);
            context.Response.Write(newFileName);
            context.Request.Files[0].SaveAs(System.IO.Path.Combine(_UploaderPath, newFileName));

            //context.Response.ContentType = "text/plain";
            //try
            //{
            //    int id = Convert.ToInt32(context.Request["id"]);
            //    int type = Convert.ToInt32(context.Request["type"]);
            //    string title = context.Request["title"];
            //    string remark = context.Request["remark"];
            //    int belong = Convert.ToInt32(context.Request["belong"]);
            //    string filePath = ConfigurationManager.AppSettings["FileUpload"].ToString() + Enum.GetName(typeof(Category), type);
            //    string a = Enum.GetName(typeof(Category), type);

            //    int index = context.Request.Files[0].FileName.LastIndexOf('.');
            //    if (index == -1) { index = 0; }
            //    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + context.Request.Files[0].FileName.Substring(index);

            //    string originalName = context.Request.Files[0].FileName;
            //    if (context.Request.Files[0].ContentLength > 0)
            //    {
            //        if (!Directory.Exists(filePath))
            //        {
            //            Directory.CreateDirectory(filePath);
            //        }
            //        context.Request.Files[0].SaveAs(filePath + "\\" + fileName);

            //        FileInsert(fileName, type, belong, title, remark, originalName);

            //        context.Response.Write("上传成功!");
            //    }
            //    else
            //    {
            //        context.Response.Write("请选择上传文件!");
            //    }
            //}
            //catch
            //{
            //    context.Response.Write("上传失败!");
            //}
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private System.Data.DataTable ReadFromExcel(string fileName)
        {
            int index = fileName.LastIndexOf('.');
            if (index == -1) { index = 0; }
            string ext = fileName.Substring(index);
            string connStr = "";
            if (ext == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            return null;
        }
    }
}