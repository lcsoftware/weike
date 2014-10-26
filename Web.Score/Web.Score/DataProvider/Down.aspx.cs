using App.Score.Data;
using App.Score.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Web.Score.DataProvider
{
    public partial class Down : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var type = int.Parse(Request["type"]);
                switch (type)
                {
                    case 1:
                        this.ExportUserGroup();
                        break;
                    default:
                        break;
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 导出用户清单
        /// </summary>
        protected void ExportUserGroup()
        {
            var schoolName = Request["school"];
            using (AppBLL bll = new AppBLL())
            {
                var sql = "select distinct Name as 用户名称,Password as 密码 from tbUserGroupInfo where  teacherid<'9999999990000' and (Status<>'1' or status is null) order by Name";
                DataTable table = bll.FillDataTableByText(sql);
                string fileName = string.Format("{0}.xls", schoolName);
                string excelHtml = DataTableToHtml(table, string.Format("{0}成绩分析系统用户清单", schoolName));
                UtilHelper.DownToExcel(fileName, excelHtml);
            }
        }
        public static string DataTableToHtml(DataTable table, string title)
        {
            //命名导出表格的StringBuilder变量
            StringBuilder sHtml = new StringBuilder(string.Empty);
            //打印表头 
            sHtml.Append("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/>");
            sHtml.Append("<table border=\"1\" width=\"100%\">");
            sHtml.Append(string.Format("<tr height=\"40\"><td colspan=\"{0}\" align=\"center\" style='font-size:24px'><b>{1}</b></td></tr>", table.Columns.Count, title));
            if (table != null && table.Rows.Count > 0)
            {
                //打印列名 
                sHtml.Append("<tr height=\"25\" align=\"center\" >");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    sHtml.Append("<td>" + table.Columns[i].ColumnName + "</td>");
                }
                sHtml.Append("</tr>");
                //打印内容
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    sHtml.Append("<tr height=\"25\" align=\"left\">");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        sHtml.Append(string.Format("<td>{0}</td>", table.Rows[i][j].ToString()));
                    }
                    sHtml.Append("</tr>");
                }
            }
            //打印表尾
            sHtml.Append("</table>");
            return sHtml.ToString();
        }
    }
}