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
                var type = !string.IsNullOrEmpty(context.Request.QueryString["type"]) ? int.Parse(context.Request.QueryString["type"]) : -1;
                switch (type)
                {
                    case 1:  //学生编号导入
                        StdImport(context);
                        break;
                    default:
                        break;
                } 
            }
            catch (Exception ex)
            {
                context.Response.Write("-1");
            }
        }

        private void StdImport(HttpContext context)
        {
            string newFileName = Guid.NewGuid().ToString().Replace("-", "");
            int index = context.Request.Files[0].FileName.LastIndexOf('.');
            if (index == -1) { index = 0; }
            newFileName += context.Request.Files[0].FileName.Substring(index);
            newFileName = System.IO.Path.Combine(_UploaderPath, newFileName);
            context.Request.Files[0].SaveAs(newFileName);
            //IList<App.Score.Entity.StudentImportEntry> students = this.ReadStudents(newFileName);
            System.Data.DataTable table = this.ReadStudents(newFileName);
            if (System.IO.File.Exists(newFileName))
            {
                System.IO.File.Delete(newFileName);
            }
            if (table.Rows.Count > 0)
            {
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(table));
            }
            else
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

        private System.Data.DataTable ReadStudents(string fileName)
        {
            int index = fileName.LastIndexOf('.');
            if (index == -1) { index = 0; }
            string ext = fileName.Substring(index);
            string connStr = string.Format("Provider=Microsoft.Jet.Oledb.4.0;Data Source={0};Extended Properties='Excel {1};HDR=YES;IMEX=1';", fileName, ext == ".xls" ? "8.0" : "12.0"); 
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connStr);
            conn.Open();
            try
            {
                //System.Data.DataTable dtSheetName = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                var sql = "Select 学年 as MicYear, 学校编号 as SchoolNo, 年级 as Grade,	班级 as GradeClass, 班内序号 as ClassN,	姓名 as Name, 性别 as Sex from [学生编号$]";
                System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(sql, conn);
                System.Data.DataTable table = new System.Data.DataTable();
                adapter.Fill(table);
                return table;
                //IList<App.Score.Entity.StudentImportEntry> students = new List<App.Score.Entity.StudentImportEntry>();
                //for (int i = 0; i < table.Rows.Count; i++)
                //{
                //    App.Score.Entity.StudentImportEntry student = new App.Score.Entity.StudentImportEntry();
                //    student.MicYear = table.Rows[i]["MicYear"].ToString();
                //    student.SchoolNo = table.Rows[i]["SchoolNo"].ToString();
                //    student.Grade = table.Rows[i]["Grade"].ToString();
                //    student.GradeClass = table.Rows[i]["GradeClass"].ToString();
                //    student.ClassN = table.Rows[i]["ClassN"].ToString();
                //    student.Name = table.Rows[i]["Name"].ToString();
                //    student.Sex = table.Rows[i]["Sex"].ToString();
                //    students.Add(student);
                //}
                //return students;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}