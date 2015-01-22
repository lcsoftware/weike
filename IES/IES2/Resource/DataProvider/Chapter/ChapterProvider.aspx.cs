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

        private static void BuildRelationChapter(IList<Chapter> allChapters, Chapter root)
        {
            var chapters = from v in allChapters where v.ParentID == root.ChapterID select v;
            foreach (var chapter in chapters)
            {
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

        private static IList<Chapter> GetNeighbors(IList<Chapter> allChapters, Chapter chapter)
        {
            IList<Chapter> brothers = new List<Chapter>();
            if (chapter.ParentID == 0)
            {
                var chapters = from v in allChapters where v.ParentID == 0 select v;
            }

            return null;
        }
        
        //章节对象排序字段步长
        private const int STEP = 500;

        [WebMethod]
        public static IList<Chapter> MoveLeft(IList<Chapter> allChapters, Chapter chapter)
        {
            //一级节点不能左移
            if (chapter.ParentID == 0) return null;
            IList<Chapter> splitChpaters = SplitChpater(allChapters, chapter);
            //找到父节点
            var parents = from v in splitChpaters where v.ChapterID == chapter.ParentID select v;
            var chapterParent = parents.First();
            var brothers = from v in splitChpaters 
                           where v.ParentID == chapterParent.ParentID && v.ChapterID >= chapterParent.ChapterID 
                           orderby v.Orde select v;
            if (brothers.Count() == 1)
            {
                chapter.Orde = brothers.First().Orde + STEP;
            }
            else
            {
                //chapter.Orde = brothers.First().Orde + brothers.
            }

            return allChapters;
        }

        private static IList<Chapter> SplitChpater(IList<Chapter> allChapters, Chapter excluder)
        {
            IList<Chapter> splitChpaters = new List<Chapter>();
            foreach (var item in allChapters)
            {
                SplitChapter(splitChpaters, item, excluder);
            }
            return splitChpaters;
        }

        private static void SplitChapter(IList<Chapter> splitChpaters, Chapter chapter, Chapter excluder)
        {
            if (chapter.ChapterID != excluder.ChapterID)
            {
                splitChpaters.Add(chapter);
            }
            foreach (var item in chapter.Children)
            {
                SplitChapter(splitChpaters, item, excluder);
            }
        }


        [WebMethod]
        public static bool Chapter_Del(Chapter model)
        {
            return new ChapterBLL().Chapter_Del(model);
        }
    }
}