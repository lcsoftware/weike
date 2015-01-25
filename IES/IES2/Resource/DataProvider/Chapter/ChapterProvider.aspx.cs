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
            //IList<Chapter> allChapters = new ChapterBLL().Chapter_List(model);
            //var roots = from v in allChapters where v.ParentID == 0 select v;
            //foreach (var chapter in roots)
            //{
            //    BuildRelationChapter(allChapters, chapter);
            //}
            //return roots.ToList();
        }

        //private static void BuildRelationChapter(IList<Chapter> allChapters, Chapter root)
        //{
        //    var chapters = from v in allChapters where v.ParentID == root.ChapterID select v;
        //    foreach (var chapter in chapters)
        //    {
        //        root.Children.Add(chapter);
        //        BuildRelationChapter(allChapters, chapter);
        //    }
        //}


        [WebMethod]
        public static Chapter Chapter_Get()
        {
            return new ChapterBLL().Chapter_Get();
        }
        [WebMethod]
        public static Chapter Chapter_ADD(IList<Chapter> chapters, Chapter model)
        {
            var brothers = from v in chapters where v.ParentID == model.ParentID orderby v.Orde select v;
            if (brothers.Any())
            {
                model.Orde = brothers.Last().Orde + STEP;
            }
            else
            {
                model.Orde = START;
            }
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

        private static IList<Chapter> GetNeighbors(IList<Chapter> allChapters, Chapter chapter)
        {
            IList<Chapter> brothers = new List<Chapter>();
            if (chapter.ParentID == 0)
            {
                var chapters = from v in allChapters where v.ParentID == 0 select v;
            }

            return null;
        }

        [WebMethod]
        public static bool Chapter_Del(Chapter model)
        {
            return new ChapterBLL().Chapter_Del(model);
        }

        //章节排序初始值
        private const int START = 100000;
        //章节对象排序字段步长
        private const int STEP = 500;

        [WebMethod]
        public static IList<Chapter> MoveLeft(IList<Chapter> allChapters, Chapter chapter)
        {
            //一级节点不能左移
            if (chapter.ParentID == 0) return null;
            var chapters = from v in allChapters where v.ChapterID == chapter.ChapterID select v;
            var ordeChapter = chapters.First();

            var parents = from v in allChapters where v.ChapterID == chapter.ParentID select v;
            var parent = parents.First();
            var brothers = from v in allChapters
                           where v.ParentID == parent.ParentID && v.ChapterID >= parent.ChapterID
                           orderby v.Orde
                           select v;
            if (brothers.Count() == 1)
            {
                ordeChapter.Orde = brothers.First().Orde + STEP;
            }
            else
            {
                var nexts = from v in brothers where v.Orde > parent.ChapterID select v;
                ordeChapter.Orde = nexts.First().Orde - 1;
            }
            new ChapterBLL().Chapter_Upd(ordeChapter);
            return allChapters;
        }

        [WebMethod]
        public static IList<Chapter> MoveRight(IList<Chapter> allChapters, Chapter chapter)
        {
            var brothers = from v in allChapters
                           where v.ParentID == chapter.ParentID &&
                                 v.Orde < chapter.Orde
                           orderby v.Orde
                           select v;
            if (brothers.Count() == 0)
            {
                //上方无节点不能右移
                return null;
            }
            var chapters = from v in allChapters where v.ChapterID == chapter.ChapterID select v;
            var ordeChapter = chapters.First();

            var previous = brothers.Last();
            var previousChildren = from v in allChapters
                                   where v.ParentID == previous.ChapterID
                                   orderby v.Orde
                                   select v;
            ordeChapter.Orde = previousChildren.Any() ? previousChildren.Last().Orde + STEP : START;

            new ChapterBLL().Chapter_Upd(ordeChapter);
            return allChapters;
        }

        [WebMethod]
        public static IList<Chapter> MoveUp(IList<Chapter> allChapters, Chapter chapter)
        {
            var chapters = from v in allChapters where v.ChapterID == chapter.ChapterID select v;
            var ordeChapter = chapters.First();
            var brothers = from v in allChapters where v.ParentID == chapter.ParentID && v.Orde < chapter.Orde select v;
            if (!brothers.Any())
            {
                return null;
            }
            var previous = brothers.Last();
            var tmp = previous.Orde;
            previous.Orde = chapter.Orde;
            chapter.Orde = tmp;
            new ChapterBLL().Chapter_Upd(ordeChapter);
            new ChapterBLL().Chapter_Upd(previous);
            return allChapters;
        }

        [WebMethod]
        public static IList<Chapter> MoveDown(IList<Chapter> allChapters, Chapter chapter)
        {
            var chapters = from v in allChapters where v.ChapterID == chapter.ChapterID select v;
            var ordeChapter = chapters.First();
            var brothers = from v in allChapters where v.ParentID == chapter.ParentID && v.Orde > chapter.Orde select v;
            if (!brothers.Any())
            {
                return null;
            }
            var next = brothers.First();
            var tmp = next.Orde;
            next.Orde = chapter.Orde;
            chapter.Orde = tmp;
            new ChapterBLL().Chapter_Upd(ordeChapter);
            new ChapterBLL().Chapter_Upd(next);
            return allChapters;
        }
    }
}