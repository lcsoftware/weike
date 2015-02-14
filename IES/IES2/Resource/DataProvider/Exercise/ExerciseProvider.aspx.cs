using IES.G2S.Resource.BLL;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
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
        public static ExerciseInfo Exercise_Model_Info_Get()
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
                },
                Children = new ExerciseInfo()
                {
                    exercisechoicelist = new List<ExerciseChoice>(),
                    exercisecommon = new ExerciseCommon()
                    {
                        kenlist = new List<Ken>(),
                        keylist = new List<Key>(),
                        exercise = new IES.Resource.Model.Exercise(),
                        attachmentlist = new List<Attachment>()
                    }
                },
                ChildrenMultiple = new ExerciseInfo()
                {
                    exercisechoicelist = new List<ExerciseChoice>(),
                    exercisecommon = new ExerciseCommon()
                    {
                        kenlist = new List<Ken>(),
                        keylist = new List<Key>(),
                        exercise = new IES.Resource.Model.Exercise(),
                        attachmentlist = new List<Attachment>()
                    }
                },
                ChildrenFillBlank = new ExerciseInfo()
                {
                    exercisechoicelist = new List<ExerciseChoice>(),
                    exercisecommon = new ExerciseCommon()
                    {
                        kenlist = new List<Ken>(),
                        keylist = new List<Key>(),
                        exercise = new IES.Resource.Model.Exercise(),
                        attachmentlist = new List<Attachment>()
                    }
                }
            };
        }

        [WebMethod]
        public static bool Exercise_ADD(string model)
        {

            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            if (v.exercisecommon.exercise.ExerciseID > 0)
            {
                bool rs = new ExerciseBLL().Exercise_Upd(v);
                if (rs)
                {
                    if (v.Children != null)
                    {
                        if (v.exercisecommon.exercise.ExerciseType == 12)
                        {
                            //v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                            return new ExerciseBLL().Exercise_Upd(v.Children);
                        }
                    }
                }
                return rs;
            }
            else
            {
                bool exerciseRs = new ExerciseBLL().Exercise_ADD(v);
                if (exerciseRs)
                {
                    if (v.Children != null)
                    {
                        if (v.exercisecommon.exercise.ExerciseType == 12)
                        {
                            v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                            return new ExerciseBLL().Exercise_ADD(v.Children);
                        }
                    }
                }
                return exerciseRs;
            }
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
            ExerciseInfo exerciseRs = new ExerciseBLL().Exercise_Custom_M_Edit(v);
            if (exerciseRs != null)
            {
                if (v.Children.exercisecommon.exercise.ExerciseType != 0)
                {
                    v.Children.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                    new ExerciseBLL().Exercise_MultipleChoice_M_Edit(v.Children);                    
                }
                if (v.ChildrenMultiple.exercisecommon.exercise.ExerciseType != 0)
                {
                    v.ChildrenMultiple.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                    new ExerciseBLL().Exercise_MultipleChoice_M_Edit(v.ChildrenMultiple);                    
                }
                if (v.ChildrenFillBlank.exercisecommon.exercise.ExerciseType != 0)
                {
                    v.ChildrenFillBlank.exercisecommon.exercise.ParentID = v.exercisecommon.exercise.ExerciseID;
                    new ExerciseBLL().Exercise_Writing_M_Edit(v.ChildrenFillBlank);
                }
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
            var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ExerciseInfo>(model);
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
    }
}