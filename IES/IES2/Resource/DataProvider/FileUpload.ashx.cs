using IES.G2S.Resource.BLL;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
                ///习题附件
                List<IES.Resource.Model.Attachment> attachMentList = IES.Service.FileService.AttachmentUpload();
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(attachMentList));
            }
            else if (from.Equals("3"))
            {
                ///习题导入功能 
                System.Data.DataTable table = IES.Service.FileService.ExerciseUpload(); 

                IES.Resource.Model.File fileModel = new IES.Resource.Model.File();
                fileModel.OCID = int.Parse(context.Request.Form["OCID"]);
                fileModel.CourseID = int.Parse(context.Request.Form["CourseID"]);
                fileModel.FileType = int.Parse(context.Request.Form["Category"]);
                DataTable resultTable = ExerciseInfo_Import(table, fileModel.OCID, fileModel.CourseID, fileModel.FileType); 
                context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(resultTable));
            }
            else
            { 
                ///资料库文件导入
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


        #region 习题导入

        /// <summary>
        /// 习题导入
        /// </summary>
        /// <param name="dt">要导入的数据</param>
        /// <param name="exerciseType">习题类型</param>
        /// <returns>true成功，false失败</returns>
        private static DataTable ExerciseInfo_Import(DataTable dt, int ocid, int courseId, int exerciseType)
        {
            if (dt.Rows.Count == 0) return null;
            switch (exerciseType)
            {
                case 7: //排序题
                case 8: //分析题
                case 9: //计算题
                case 14: //阅读理解题
                case 18: //简答题
                case 12: //听力题
                case 17: //自定义题
                case 6:  //连线题
                    break;
                case 10: //问答题
                case 11: //翻译题
                case 4: //名词解释
                case 13: //写作题
                    return ExerciseImportA(dt, ocid, courseId);
                case 1: //判断题
                    return ExerciseImportB(dt, ocid, courseId);
                case 5: //填空题
                    return ExerciseImportC(dt, ocid, courseId);
                case 2: //单选题
                case 3: //多选题
                    return ExerciseImportC(dt, ocid, courseId);
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 构造习题导入结果集
        /// </summary>
        /// <returns></returns>
        private static DataTable BuildResultTable()
        {
            DataTable resultDt = new DataTable();
            resultDt.Columns.Add("Conten");
            resultDt.Columns.Add("Message");
            resultDt.Columns.Add("Status");
            return resultDt;
        }
        private static DataTable ExerciseImportA(DataTable dt, int ocid, int courseId)
        {
            DataTable resultTable = BuildResultTable();
            ExerciseInfo exercise;
            if (dt.Rows.Count == 0) return resultTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    exercise = ExerciseBind(dt.Rows[i]);
                    exercise.exercisecommon.exercise.Conten = dt.Rows[i]["习题内容"].ToString();
                    exercise.exercisecommon.exercise.Analysis = dt.Rows[i]["习题解析"].ToString();
                    exercise.exercisecommon.exercise.Answer = dt.Rows[i]["答案"].ToString();
                    Exercise_Writing_M_Edit(Newtonsoft.Json.JsonConvert.SerializeObject(exercise));
                    resultTable.Rows[i]["Message"] = "导入成功";
                    resultTable.Rows[i]["Status"] = "1";
                }
                catch (Exception ex)
                {
                    resultTable.Rows[i]["Message"] = ex.Message;
                    resultTable.Rows[i]["Status"] = "-1";
                }
                resultTable.Rows[i]["Conten"] = dt.Rows[i]["习题内容"].ToString();
            }
            return resultTable;
        }

        private static DataTable ExerciseImportB(DataTable dt, int ocid, int courseId)
        {
            DataTable resultTable = BuildResultTable();
            ExerciseInfo exercise;
            if (dt.Rows.Count == 0) return resultTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    exercise = ExerciseBind(dt.Rows[i]);
                    exercise.exercisecommon.exercise.Conten = dt.Rows[i]["习题内容"].ToString();
                    exercise.exercisecommon.exercise.Analysis = dt.Rows[i]["习题解析"].ToString();
                    Exercise_Judge_M_Edit(Newtonsoft.Json.JsonConvert.SerializeObject(exercise));
                    resultTable.Rows[i]["Message"] = "导入成功";
                    resultTable.Rows[i]["Status"] = "1";
                }
                catch (Exception ex)
                {
                    resultTable.Rows[i]["Message"] = ex.Message;
                    resultTable.Rows[i]["Status"] = "-1";
                }
                resultTable.Rows[i]["Conten"] = dt.Rows[i]["习题内容"].ToString();
            }
            return resultTable;
        }

        private static DataTable ExerciseImportC(DataTable dt, int ocid, int courseId)
        {
            DataTable resultTable = BuildResultTable();
            ExerciseInfo exercise;
            if (dt.Rows.Count == 0) return resultTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string content = "";
                    exercise = ExerciseBind(dt.Rows[i]);
                    exercise.exercisecommon.exercise.Conten = dt.Rows[i]["习题内容"].ToString();
                    exercise.exercisecommon.exercise.Analysis = dt.Rows[i]["习题解析"].ToString();
                    for (int n = 0; n < 6; n++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["答案" + (n + 1)].ToString())) continue;
                        string alternative = dt.Rows[i]["答案" + (n + 1) + "（备选）"].ToString() == "" ? ((char)32).ToString() : dt.Rows[i]["答案" + (n + 1) + "（备选）"].ToString();
                        //判断下一题是不是空
                        if (!string.IsNullOrEmpty(dt.Rows[i]["答案" + (n + 1)].ToString()) ||
                            string.IsNullOrEmpty(dt.Rows[i]["答案" + (n + 2)].ToString()))
                        {
                            content += "0wshgkjqbwhfbxlfrh_b" + dt.Rows[i]["答案" + (n + 1)].ToString()
                            + "wshgkjqbwhfbxlfrh_c" + alternative;
                        }
                        else
                        {
                            content += "0wshgkjqbwhfbxlfrh_b" + dt.Rows[i]["答案" + (n + 1)].ToString()
                            + "wshgkjqbwhfbxlfrh_c" + alternative + "wshgkjqbwhfbxlfrh_a";
                        }
                    }
                    exercise.exercisecommon.exercise.Content = content;
                    Exercise_FillInBlanks_M_Edit(Newtonsoft.Json.JsonConvert.SerializeObject(exercise));
                    resultTable.Rows[i]["Message"] = "导入成功";
                    resultTable.Rows[i]["Status"] = "1";
                }
                catch (Exception ex)
                {
                    resultTable.Rows[i]["Message"] = ex.Message;
                    resultTable.Rows[i]["Status"] = "-1";
                }
                resultTable.Rows[i]["Conten"] = dt.Rows[i]["习题内容"].ToString();
            }
            return resultTable;
        }

        private static DataTable ExerciseImportD(DataTable dt, int ocid, int courseId)
        {
            DataTable resultTable = BuildResultTable();
            ExerciseInfo exercise;
            if (dt.Rows.Count == 0) return resultTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string content = "";
                    exercise = ExerciseBind(dt.Rows[i]);
                    exercise.exercisecommon.exercise.Conten = dt.Rows[i]["习题内容"].ToString();
                    exercise.exercisecommon.exercise.Analysis = dt.Rows[i]["习题解析"].ToString();
                    int[] answer = new int[dt.Rows[i]["正确答案"].ToString().Length];

                    for (int n = 0; n < dt.Rows[i]["正确答案"].ToString().Length; n++)
                    {
                        answer[i] = Convert.ToInt32(dt.Rows[i]["正确答案"].ToString().Substring(i, 1));
                    }

                    for (int n = 0; n < 8; n++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["选项" + (n + 1)].ToString())) continue;
                        int correct = 0;
                        foreach (var a in answer)
                        {
                            if (a == (n + 1))
                            {
                                correct = 1;
                            }
                        }
                        //判断下一题是不是空
                        if (!string.IsNullOrEmpty(dt.Rows[i]["选项" + (n + 1)].ToString()) ||
                            string.IsNullOrEmpty(dt.Rows[i]["选项" + (n + 2)].ToString()))
                        {
                            content += "0wshgkjqbwhfbxlfrh_b" + dt.Rows[i]["选项" + (n + 1)].ToString()
                            + "wshgkjqbwhfbxlfrh_c" + correct;
                        }
                        else
                        {
                            content += "0wshgkjqbwhfbxlfrh_b" + dt.Rows[i]["选项" + (n + 1)].ToString()
                            + "wshgkjqbwhfbxlfrh_c" + correct + "wshgkjqbwhfbxlfrh_a";
                        }

                    }
                    exercise.exercisecommon.exercise.Content = content;
                    Exercise_FillInBlanks_M_Edit(Newtonsoft.Json.JsonConvert.SerializeObject(exercise));
                    resultTable.Rows[i]["Message"] = "导入成功";
                    resultTable.Rows[i]["Status"] = "1";
                }
                catch (Exception ex)
                {
                    resultTable.Rows[i]["Message"] = ex.Message;
                    resultTable.Rows[i]["Status"] = "-1";
                }
                resultTable.Rows[i]["Conten"] = dt.Rows[i]["习题内容"].ToString();
            }
            return resultTable;
        }
        private static ExerciseInfo ExerciseBind(DataRow dr)
        {
            ExerciseInfo exercise;
            string[] Keys;
            string[] Kens;
            int scope = 0;
            string key = "";
            string ken = "";

            exercise = new ExerciseInfo();
            exercise.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            exercise.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            exercise.exercisecommon.exercise.Diffcult = Convert.ToInt32(dr["难易程度"]);
            //exercise.exercisecommon.exercise.ShareRange = Convert.ToInt32(dr["ShareRange"]);
            //exercise.exercisecommon.exercise.OCID = Convert.ToInt32(dr["OCID"]);
            //exercise.exercisecommon.exercise.CourseID = Convert.ToInt32(dr["CourseID"]);
            //exercise.exercisecommon.exercise.Chapter = Convert.ToInt32(dr["Chapter"]);
            if (dr["适用范围"] != null)
            {
                scope += Convert.ToInt32(dr["适用范围"].ToString());
            }
            if (dr["关键字"] != null)
            {
                Keys = dr["关键字"].ToString().Split(',');
                for (int i = 0; i < Keys.Length; i++)
                {
                    key += Keys.GetValue(i).ToString() + "wshgkjqbwhfbxlfrh";
                }
            }
            if (dr["知识点"] != null)
            {
                Kens = dr["知识点"].ToString().Split(',');
                for (int i = 0; i < Kens.Length; i++)
                {
                    ken += Kens.GetValue(i).ToString() + "wshgkjqbwhfbxlfrh";
                }
            }
            return exercise;
        }

        private static ExerciseInfo Exercise_Writing_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Writing_M_Edit(v);
        }

        private static ExerciseInfo Exercise_FillInBlanks_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_FillInBlanks_M_Edit(v);
        }

        private static ExerciseInfo Exercise_Judge_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Judge_M_Edit(v);
        }
        #endregion



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}