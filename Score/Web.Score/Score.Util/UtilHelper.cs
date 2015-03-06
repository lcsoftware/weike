using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Score.Util
{
    public class UtilHelper
    {

        public static void DownToExcel(string fileName, string content)
        {
            String agent = System.Web.HttpContext.Current.Request.UserAgent;
            System.Web.HttpContext.Current.Response.Charset = "UTF-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (agent.Contains("Firefox"))
            {
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            }
            else
            {
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
            }
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel;charset=UTF-8";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.HttpContext.Current.Response.Output.Write(content);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
        public static void DownToBinary(string fileName, string content)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("gb2312").GetBytes(content);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.AddHeader("Content-Length", bytes.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
        }

        public static void WriteContent(string fileName, string content)
        {
            FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            try
            {
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine(content);
                sw.Close();
            }
            finally
            {
                aFile.Close();
            }
        }
        
    }
}
