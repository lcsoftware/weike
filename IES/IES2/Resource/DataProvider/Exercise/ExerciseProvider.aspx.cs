﻿using IES.G2S.Resource.BLL;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Resource.DataProvider.Exercise
{
    public partial class ExerciseProvider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static List<IES.Resource.Model.Exercise> Exercise_Search(IES.Resource.Model.Exercise model, Key key, string keys, string kens, int pageSize, int pageIndex)
        {
            model.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            return new ExerciseBLL().Exercise_Search(model, key, keys, kens, pageSize, pageIndex);
        }

        [WebMethod]
        public static bool Attachment_SourceID_Upd(Attachment model)
        {
            return AttachmentBLL.Attachment_SourceID_Upd(model);
        }

        [WebMethod]
        public static ExerciseCommon Exercise_Model_Info()
        {
            return new ExerciseCommon()
            {
                kenlist = new List<Ken>(),
                keylist = new List<Key>(),
                exercise = new IES.Resource.Model.Exercise(),
                attachmentlist = new List<Attachment>()
            };
        }

        [WebMethod]
        public static ExerciseInfo Exercise_Model_Get()
        {
            return new ExerciseInfo()
            {
                exercisechoicelist = new List<ExerciseChoice>(),
                exercisecommon = new ExerciseCommon()
                {
                    kenlist = new List<Ken>(),
                    keylist = new List<Key>(),
                    exercise = new IES.Resource.Model.Exercise(),
                    attachmentlist = new List<Attachment>()
                }
            };
        }

        [WebMethod]
        public static ExerciseInfo Exercise_Model_Info_Get()
        {
            ExerciseInfo info = new ExerciseInfo();

            info.exercisechoicelist = new List<ExerciseChoice>();
            info.exercisecommon = new ExerciseCommon()
                {
                    kenlist = new List<Ken>(),
                    keylist = new List<Key>(),
                    exercise = new IES.Resource.Model.Exercise(),
                    attachmentlist = new List<Attachment>()
                };
            //单选
            info.Children = new List<ExerciseInfo>();
            info.Children.Add(new ExerciseInfo()
            {
                exercisechoicelist = new List<ExerciseChoice>(),
                exercisecommon = new ExerciseCommon()
                {
                    kenlist = new List<Ken>(),
                    keylist = new List<Key>(),
                    exercise = new IES.Resource.Model.Exercise(),
                    attachmentlist = new List<Attachment>()
                }
            });
            //多选
            info.ChildrenMultiple = new List<ExerciseInfo>();
            info.ChildrenMultiple.Add(new ExerciseInfo()
            {
                exercisechoicelist = new List<ExerciseChoice>(),
                exercisecommon = new ExerciseCommon()
                {
                    kenlist = new List<Ken>(),
                    keylist = new List<Key>(),
                    exercise = new IES.Resource.Model.Exercise(),
                    attachmentlist = new List<Attachment>()
                }
            });
            //填空
            info.ChildrenFillBlank = new List<ExerciseInfo>();
            info.ChildrenFillBlank.Add(new ExerciseInfo()
            {
                exercisechoicelist = new List<ExerciseChoice>(),
                exercisecommon = new ExerciseCommon()
                {
                    kenlist = new List<Ken>(),
                    keylist = new List<Key>(),
                    exercise = new IES.Resource.Model.Exercise(),
                    attachmentlist = new List<Attachment>()
                }
            });

            return info;
        }

        //[WebMethod]
        //public static bool Exercise_ADD(string model)
        //{

        //    var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
        //    if (v.exercisecommon.exercise.ExerciseID > 0)
        //    {
        //        bool rs = new ExerciseBLL().Exercise_Upd(v);
        //        if (rs)
        //        {
        //            if (v.Children != null)
        //            {
        //                if (v.exercisecommon.exercise.ExerciseType == 12)
        //                {
        //                    //v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
        //                    return new ExerciseBLL().Exercise_Upd(v.Children);
        //                }
        //            }
        //        }
        //        return rs;
        //    }
        //    else
        //    {
        //        bool exerciseRs = new ExerciseBLL().Exercise_ADD(v);
        //        if (exerciseRs)
        //        {
        //            if (v.Children != null)
        //            {
        //                if (v.exercisecommon.exercise.ExerciseType == 12)
        //                {
        //                    v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
        //                    return new ExerciseBLL().Exercise_ADD(v.Children);
        //                }
        //            }
        //        }
        //        return exerciseRs;
        //    }
        //}

        /// <summary>
        /// 习题导入
        /// </summary>
        /// <param name="dt">要导入的数据</param>
        /// <param name="exerciseType">习题类型</param>
        /// <returns>true成功，false失败</returns>
        [WebMethod]
        public static bool ExerciseInfo_Import(DataTable dt, int exerciseType)
        {
            try
            {
                ExerciseInfo exercise;
                string[] scopes;
                string[] Keys;
                string[] Kens;
                int scope = 0;
                string key = "";
                string ken = "";
                string Content = "";
                if (dt.Rows.Count == 0) return false;
                switch (exerciseType)
                {
                    case 18: //简答题
                        break;
                    case 4: //名词解释
                        break;
                    case 12: //听力题
                        break;
                    case 17: //自定义题
                        break;
                    case 10: //问答题
                        break;
                    case 13: //写作题
                        break;
                    case 1: //判断题
                        break;
                    case 5: //填空题
                        break;
                    case 6:  //连线题
                        break;
                    case 2: //单选题，多选题
                    case 3: //多选题
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            exercise = ExerciseBind(dt.Rows[i]);
                            exercise.exercisecommon.exercise.Conten = dt.Rows[i]["习题内容"].ToString();
                            exercise.exercisecommon.exercise.Analysis = dt.Rows[i]["习题解析"].ToString();
                            if(dt.Rows[i]["选项1"].ToString()!="")
                            {
                                
                            }
                        }

                        break;
                    case 11: //翻译题
                        break;
                    case 7: //排序题
                        break;
                    case 8: //分析题
                        break;
                    case 9: //计算题
                        break;
                    case 14: //阅读理解题
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static ExerciseInfo ExerciseBind(DataRow dr)
        {
            ExerciseInfo exercise;
            string[] scopes;
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
                scopes = dr["适用范围"].ToString().Split(',');
                for (int i = 0; i < scopes.Length; i++)
                {
                    scope += Convert.ToInt32(scopes.GetValue(i));
                }
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

        [WebMethod]
        public static ExerciseInfo ExerciseInfo_Get(int model)
        {
            var v = new ExerciseInfo();
            v.ExerciseID = model;
            return new ExerciseBLL().ExerciseInfo_Get(v);
        }

        [WebMethod]
        public static ExerciseInfo ExerciseInfo_GetListen(int model)
        {
            var v = new ExerciseInfo();
            v.ExerciseID = model;
            return new ExerciseBLL().ExerciseInfo_GetListen(v);
        }

        [WebMethod]
        public static bool Exercise_Del(int exerciseID)
        {
            var v = new IES.Resource.Model.Exercise() { ExerciseID = exerciseID };
            return new ExerciseBLL().Exercise_Del(v);
        }

        /// <summary>
        /// 判断题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Judge_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Judge_M_Edit(v);
        }
        /// <summary>
        /// 判断题信息获取
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Judge_Get(int ExerciseID)
        {
            return new ExerciseBLL().Exercise_Judge_Get(ExerciseID);
        }

        /// <summary>
        /// 单选 多选题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_MultipleChoice_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_MultipleChoice_M_Edit(v);
        }
        /// <summary>
        /// 单选 多选题信息获取
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_MultipleChoice_Get(int ExerciseID)
        {
            return new ExerciseBLL().Exercise_MultipleChoice_Get(ExerciseID);
        }


        /// <summary>
        /// 填空题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_FillInBlanks_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_FillInBlanks_M_Edit(v);
        }

        /// <summary>
        /// 填空题 等习题的获取
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Analysis_Get(int ExerciseID)
        {
            return new ExerciseBLL().Exercise_Analysis_Get(ExerciseID);
        }

        /// <summary>
        /// 名词解释、 分析题、解答题、计算题 基本信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Analysis_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Analysis_M_Edit(v);
        }
        /// <summary>
        /// 问答题、写作题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Writing_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Writing_M_Edit(v);
        }
        /// <summary>
        /// 问答题、 写作题详细信息
        /// </summary>
        /// <param name="ExerciseID"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Writing_Get(int ExerciseID)
        {
            return new ExerciseBLL().Exercise_Writing_Get(ExerciseID);
        }
        /// <summary>
        /// 听力，自定义信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Custom_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            ExerciseInfo exerciseRs = new ExerciseBLL().Exercise_Custom_M_Edit(v);
            if (exerciseRs != null)
            {
                foreach (var item in v.Children)
                {
                    if (item.exercisecommon.exercise.ExerciseType != 0)
                    {
                        item.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                        new ExerciseBLL().Exercise_MultipleChoice_M_Edit(item);
                    }
                }
                foreach (var item in v.ChildrenMultiple)
                {
                    if (item.exercisecommon.exercise.ExerciseType != 0)
                    {
                        item.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                        new ExerciseBLL().Exercise_MultipleChoice_M_Edit(item);
                    }
                }
                foreach (var item in v.ChildrenFillBlank)
                {
                    if (item.exercisecommon.exercise.ExerciseType != 0)
                    {
                        item.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                        new ExerciseBLL().Exercise_Writing_M_Edit(item);
                    }
                }
                //if (v.Children.exercisecommon.exercise.ExerciseType != 0)
                //{
                //    v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                //    new ExerciseBLL().Exercise_MultipleChoice_M_Edit(v.Children);
                //}
                //if (v.ChildrenMultiple.exercisecommon.exercise.ExerciseType != 0)
                //{
                //    v.ChildrenMultiple.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                //    new ExerciseBLL().Exercise_MultipleChoice_M_Edit(v.ChildrenMultiple);
                //}
                //if (v.ChildrenFillBlank.exercisecommon.exercise.ExerciseType != 0)
                //{
                //    v.ChildrenFillBlank.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                //    new ExerciseBLL().Exercise_Writing_M_Edit(v.ChildrenFillBlank);
                //}
            }
            return exerciseRs;
        }

        /// <summary>
        /// 排序题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Order_M_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Order_M_Edit(v);
        }

        /// <summary>
        /// 连线题信息维护
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [WebMethod]
        public static ExerciseInfo Exercise_Line_S_Edit(string model)
        {
            ExerciseInfo v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            v.exercisecommon.exercise.CreateUserID = IES.Service.UserService.CurrentUser.UserID;
            v.exercisecommon.exercise.CreateUserName = IES.Service.UserService.CurrentUser.UserName;
            return new ExerciseBLL().Exercise_Line_S_Edit(v);
        }

        [WebMethod]
        public static ExerciseInfo Exercise_Custom_Get(int ExerciseID)
        {
            return new ExerciseBLL().Exercise_Custom_Get(ExerciseID);
        }

        [WebMethod]
        public static bool Exercise_Batch_ShareRange(string ids, int sharerange)
        {
            return new ExerciseBLL().Exercise_Batch_ShareRange(ids, sharerange);
        }

        [WebMethod]
        public static bool Exercise_Batch_Del(string ids)
        {
            return new ExerciseBLL().Exercise_Batch_Del(ids);
        }

        [WebMethod]
        public static bool Exercise_Batch_Diffcult(string ids, int diffcult)
        {
            return new ExerciseBLL().Exercise_Batch_Diffcult(ids, diffcult);
        }
    }
}