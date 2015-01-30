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
            return new ExerciseInfo() { exercisechoicelist = new List<ExerciseChoice>(), exercisecommon = new ExerciseCommon() };            
        }

        [WebMethod]
        public static bool Exercise_ADD(ExerciseInfo model)
        {
            return new ExerciseBLL().Exercise_ADD(model);            
        }
    }
}