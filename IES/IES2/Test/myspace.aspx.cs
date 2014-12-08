using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Resource.Model;
using IES.G2S.Resource.BLL;
using IES.G2S.Resource.IBLL;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using IES.AOP.G2S;

namespace Test
{
    public partial class myspace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //记录日志 示例
                AopServerFactory.getLogServer().Info(new IESLogConent(1, 1, 1, 1, 1, "127.0.0.1", "Root 开始记录日志"));
                IExercise model = new Exercise();
                model.ExerciseID = 2;
                //拦截调用 示例
                IExerciseBLL bll = AopServerFactory.getExerciseServer().GetServer<IExerciseBLL>("IExerciseBLL");
                bll.Exercise_Get(model);
                AopServerFactory.getLogServer("exercise_logger").Info(new IESLogConent(1, 1, 1, 1, 1, "127.0.0.1", "Exercise 开始记录日志")); ;

            }
            catch (Exception ex)
            {
                Response.Write("错误:" + ex.Message);
                AopServerFactory.getLogServer().Error(1, 2, 3, 4, 5, "127.0.0.1", "Root Error: " + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //拦截调用 示例
            IFileBLL bll = AopServerFactory.getJwServer().GetServer<IFileBLL>("IFileBLL");
            Folder folder = new Folder();
            folder.FolderID = 1;
            bll.Folder_Del(folder);

            //记录日志 示例
            AopServerFactory.getLogServer().Info(new IESLogConent(1, 1, 1, 1, 1, "127.0.0.1", "root 他调用了Folder_Del"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //IFileBLL bll = AopServerFactory.getJwServer("TwoClass").GetServer<IFileBLL>("IFileBLL");
            //Folder folder = new Folder();
            //folder.FolderID = 1;
            //bll.Folder_Del(folder);

            //FileBLL bll1 = new FileBLL();
            //Folder folder = new Folder();
            //folder.FolderID = 1;
            //List<Folder> list = new List<Folder>();
            //list.Add(folder);
            //bll1.Folder_Batch_Del(list);


            ExerciseCommon ec = new ExerciseCommon();
            Exercise exercise = new Exercise();

            exercise.OwnerUserID = 1 ;
            exercise.IsRand = true ;
            exercise.Analysis = string.Empty;
            exercise.Answer = string.Empty;
            exercise.Conten = string.Empty;
            exercise.CourseID = 1;
            exercise.CreateUserID = 1;
            exercise.ExerciseType = 1;
            exercise.ParentID = 0;
            exercise.Scope = 0;
            exercise.ScorePoint = string.Empty ;
            exercise.ShareRange = 1 ;

            ec.exercise = exercise ;

            ExerciseBLL bll = new ExerciseBLL();

            Ken ken = new Ken();
            ken.Name = "aaa";
            ken.KenID = 0;

            List<Ken> kenlist = new List<Ken>();
            kenlist.Add(ken);

            ec.kenlist = kenlist;

            ExerciseInfo ei = new ExerciseInfo();
            ei.exercisecommon = ec;

            List<ExerciseChoice> choicelist = new List<ExerciseChoice>();
            ExerciseChoice choice = new ExerciseChoice();
            choice.ExerciseID = 0;
            choice.ChoiceID = 0;
            choice.Conten = "a";
            choice.Grou = 1;
            choice.IsCorrect = true;
            choicelist.Add(choice);
            ei.exercisechoicelist = choicelist;

            bll.Exercise_ADD(ei);






       //     bll.Exercise_ADD(ec);

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            IES.Common.Data.ResourceCommonData rc = new IES.Common.Data.ResourceCommonData();
            List<ResourceDict>   List =   rc.Resource_Dict_Diffcult_Get();


        }
    }
}