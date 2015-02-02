/* **************************************************************
 * Copyright(c) 2014 IES, All Rights Reserved.   
 * File             : ChapterProvider.aspx.cs
 * Description      : 章节数据访问
 * Author           : zhaotianyu
 * Created          : 2015-01-17  
 * Revision History : 
******************************************************************/
namespace App.Resource.DataProvider.Chapter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using IES.Resource.Model;
    using IES.G2S.Resource.BLL;
    public partial class ChapterProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static IList<Chapter> Chapter_List(Chapter model)
        {
            return new ChapterBLL().Chapter_List(model); 
        } 

        [WebMethod]
        public static Chapter Chapter_ADD(Chapter model)
        {
            return new ChapterBLL().Chapter_ADD(model);
        }

        [WebMethod]
        public static bool Chapter_Upd(Chapter model)
        {
            return new ChapterBLL().Chapter_Upd(model);
        }

        [WebMethod]
        public static bool Chapter_Batch_Upd(IList<Chapter> models)
        {
            var bll = new ChapterBLL();
            foreach (var model in models)
            {
                bll.Chapter_Upd(model);
            }
            return true;
        }     

        [WebMethod]
        public static bool Chapter_Del(Chapter model)
        {
            return new ChapterBLL().Chapter_Del(model);
        }

        private static Chapter FindByID(IList<Chapter> allChapters, int chapterID)
        {
            foreach (var item in allChapters)
            {
                if (item.ChapterID == chapterID) return item;
            }
            return null;
        }

        [WebMethod]
        public static IList<Chapter> Chapter_Move(Chapter model, string direction)
        {
            var BLL = new ChapterBLL();
            bool moved = BLL.Chapter_Move(model.ChapterID, direction);
            return moved ? BLL.Chapter_List(model) : null; 
        }


        [WebMethod]
        public static List<IES.Resource.Model.File> Chapter_File_List(int chapterId, int kenId)
        {
            Chapter chapter = new Chapter();
            chapter.ChapterID = chapterId;
            chapter.CreateUserID = IES.Service.UserService.CurrentUser.UserID; 
            Ken ken = new Ken() { KenID = kenId };
            return new ChapterBLL().Chapter_File_List(chapter, ken);
        }

        [WebMethod]
        public static IList<Exercise> Chapter_Exercise_List(int chapterId, int kenId)
        {
            Chapter chapter = new Chapter();
            chapter.ChapterID = chapterId;
            chapter.CreateUserID = IES.Service.UserService.CurrentUser.UserID; 
            Ken ken = new Ken() { KenID = kenId };
            return new ChapterBLL().Chapter_Exercise_List(chapter, ken);
        }
    }
}