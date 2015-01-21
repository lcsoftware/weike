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
            IList<Chapter> allChapters = new ChapterBLL().Chapter_List(model);
            var roots = from v in allChapters where v.ParentID == 0 select v;
            foreach (var chapter in roots)
            {
                BuildRelationChapter(allChapters, chapter);
            }
            return roots.ToList();
        }

        private static void BuildRelationChapter(IList<Chapter> allChapters, Chapter root)
        {
            var chapters = from v in allChapters where v.ParentID == root.ChapterID select v;
            foreach (var chapter in chapters)
            {
                chapter.ChapterParent = root;
                root.Children.Add(chapter);
                BuildRelationChapter(allChapters, chapter);
            }
        }


        [WebMethod]
        public static Chapter Chapter_Get()
        {
            return new ChapterBLL().Chapter_Get();
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

        //[WebMethod]
        //public static IList<Chapter> ChapterMoveLeft(IList<Chapter> allChapters, Chapter chapter)
        //{
        //    var chapters = from v in allChapters where v 

        //    return allChapters;
        //} 

        [WebMethod]
        public static bool Chapter_Del(Chapter model)
        {
            return new ChapterBLL().Chapter_Del(model);
        }
    }
}