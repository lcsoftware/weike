// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Microsoft">
//   Copyright ?2014 Microsoft
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace App.G2S
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //公共框架js 这个必须要加载的
            bundles.Add(new ScriptBundle("~/js/framework").Include(
                    "~/js/jquery-1.8.3.min.js",
                    //"~/js/jquery-1.7.1.min.js",
                //"~/Frameworks/jquery/jquery-1.11.1.min.js",  //这个jq版本过低了 不兼容一些需要jq的框架
                    "~/Frameworks/bootstrap/js/bootstrap.min.js",
                    "~/Frameworks/angular/angular.js",
                    "~/Frameworks/angular/angular-ui-router.min.js",
                    "~/Frameworks/angular/angular-cookies.min.js",
                    "~/Frameworks/bootstrap/js/bootstrap-modal.js",
                     "~/Frameworks/bootstrap/js/angular-ui-tree.js",
                    "~/Frameworks/bootstrap/js/bootstrap-transition.js",
                //分页控件
                     "~/Frameworks/laypage/laypage.js",
                     "~/Frameworks/My97DatePicker/WdatePicker.js",
                //end
                //日期控件
                    "~/Frameworks/laydate/laydate.js",
                //end
                //统计图
                "~/Frameworks/echarts-2.0.1/echarts-plain-map.js",
                //end
                "~/Views/CourseLive/Forum/file_upload_plug-in.js",
                "~/Views/CourseLive/Forum/uploadfile.js"
                ));
            //公共框架js 必须要有的
            bundles.Add(new ScriptBundle("~/js/app").Include(
                  "~/scripts/common/directives.js",
                  "~/scripts/common/filters.js",
                  "~/scripts/common/services.js",
                  "~/scripts/controllers/OC/Site/SiteController.js",
                  "~/scripts/controllers/OC/FC/FCController.js",
                  "~/scripts/controllers/OC/Class/ClassController.js",
                  "~/scripts/controllers/OC/MOOC/MOOCController.js",
                  "~/scripts/controllers/OC/Team/TeamController.js",
                  "~/scripts/controllers/home/HomeController.js",
                  "~/scripts/controllers/user/UserController.js",
                  "~/scripts/services/user/UserService.js",
                  "~/scripts/controllers/CourseLive/Forum/ForumController.js",
                  "~/scripts/controllers/CourseLive/Score/ScoreController.js",
                  "~/scripts/controllers/CourseLive/Test/TestController.js",
                  "~/scripts/controllers/CourseLive/Test/MarkingController.js",
                  "~/scripts/controllers/CourseLive/Test/DoHomeWork.js",
                  "~/scripts/controllers/OC/MOOC/MOOCPreviewController.js",
                  "~/scripts/controllers/CourseLive/Test/HomeWorkController.js",
                  "~/scripts/controllers/Affairs/AffairsController.js",
                  "~/scripts/controllers/Affairs/StudyProcessController.js",
                   "~/scripts/controllers/OC/CourseIndexController.js",
                  "~/scripts/app.js"




              ));
            //对应_Layout.cshtml 的样式
            bundles.Add(new StyleBundle("~/content/css/Layout").Include(
                   "~/Css/footer.css",
                   "~/Css/header.css",
                //"~/Css/index.css",
                   "~/Css/side_left.css",
                   //"~/Css/reverse.css", //非公共样式 去掉
                   "~/Frameworks/bootstrap/css/bootstrap.css",
                   "~/Css/common.css",
                // "~/Css/class.css",//非公共样式 去掉
                //分页插件样式
                   "~/Frameworks/laypage/skin/laypage.css",
                //end
                   "~/Frameworks/laydate/need/laydate.css",
                   "~/Frameworks/laydate/skin/default/laydate.css",
                   "~/Frameworks/laydate/skin/molv/laydate.css"
               ));
            //对应_Layout.cshtml 的js
            bundles.Add(new ScriptBundle("~/js/Layout").Include(
                  
                  "~/js/G2S.js",
                // "~/js/TopMaster.js",
                //   "~/js/leftMaster.js",
                //   "~/js/FootMaster.js",
                 // "~/js/index.js"
                 "~/js/common.js"
                 // "~/js/construction.js" //这个和index.js冲突 所以不需要它 guokaiju 
                //"~/Scripts/directive/datepicker.js",
                //"~/js/bootstrap.js",
                //"~/js/bootstrap-datepicker.js"
                ));


            //对应_Layout2.cshtml 的样式
            bundles.Add(new StyleBundle("~/content/css/Layout2").Include(
                 "~/Frameworks/bootstrap/js/angular-ui-tree.min.css",
                //"~/Css/construction.css",
                   "~/Frameworks/bootstrap/css/bootstrap.css"
               ));

            //对应_Layout2.cshtml  的js
            bundles.Add(new ScriptBundle("~/js/Layout2").Include(

                "~/js/G2S.js",
                "~/js/construction.js"
            ));

            //对应_Layout3.cshtml 的样式
            bundles.Add(new StyleBundle("~/content/css/Layout3").Include(
                   "~/Css/footer.css",
                   "~/Css/header.css",
                //"~/Css/index.css",
                 //  "~/Css/side_left.css",
                  // "~/Css/reverse.css",
                //"~/Frameworks/bootstrap/css/bootstrap.css",
                  // "~/Css/datepicker.css",
                   "~/Frameworks/bootstrap/css/bootstrap.css",
                   "~/Css/common.css"
            ));
        }
    }
}
