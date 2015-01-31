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
        public static List<IES.Resource.Model.Exercise> Exercise_Search(IES.Resource.Model.Exercise model, Key key, int pageSize, int pageIndex)
        {
            return new ExerciseBLL().Exercise_Search(model, key, pageSize, pageIndex);
        }

        [WebMethod]
        public static ExerciseInfo Exercise_Model_Info()
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
    }
}