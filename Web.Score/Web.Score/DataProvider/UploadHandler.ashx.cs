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
            try
            {
                //string newFileName = Guid.NewGuid().ToString().Replace("-", "");
                //int index = context.Request.Files[0].FileName.LastIndexOf('.');
                //if (index == -1) { index = 0; }
                //newFileName += context.Request.Files[0].FileName.Substring(index);
                //context.Request.Files[0].SaveAs(System.IO.Path.Combine(_UploaderPath, newFileName));
                //context.Response.Write(newFileName);
<<<<<<< HEAD

                ReadFromExcel(@"C:\Users\devWin\Desktop\ff.xls");

            }
            catch(Exception ex)
=======

                ReadFromExcel(@"f:\fff.xls");



            }
            catch (Exception ex)
>>>>>>> c437ce16a8362157adc306f57390cb14a56f3dd6
            {
                context.Response.Write("-1");
            }
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
<<<<<<< HEAD
            string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};HDR=YES;IMEX=1;Extended Properties=\"{1}\"", fileName, ext == ".xls" ? "8.0" : "12.0");
=======
            //string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};HDR=YES;IMEX=1;Extended Properties=\"{1}\"", fileName, ext == ".xls" ? "8.0" : "12.0");
            string connStr = string.Format("Provider=Microsoft.Jet.Oledb.4.0;Data Source={0};Extended Properties='Excel {1};HDR=no;IMEX=1';", fileName, ext == ".xls" ? "8.0" : "12.0");
>>>>>>> c437ce16a8362157adc306f57390cb14a56f3dd6
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connStr);
            conn.Open();
            System.Data.DataTable dtSheetName = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            return dtSheetName;
        }
    }
}