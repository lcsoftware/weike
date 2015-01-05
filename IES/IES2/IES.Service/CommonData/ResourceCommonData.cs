using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;
using IES.Cache;
namespace IES.Common.Data
{
    /// <summary>
    /// 获取资源模块的一些通用属性
    /// </summary>
    public class ResourceCommonData
    {
        /// <summary>
        /// 获取所有的资源库下的字典表
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_Get()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "ResourceDict"))
            {
                List<ResourceDict> resourcedictlist = CommonDataDAL.ResourceDict_List();
                cache.Set(string.Empty, "ResourceDict", resourcedictlist);
                return resourcedictlist;
            }
            else
            {
                return cache.Get<List<ResourceDict>>(string.Empty, "ResourceDict");
            }
        }

        /// <summary>
        /// 获取习题的题型
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_ExerciseType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Exercise.ExerciseType")).ToList<ResourceDict>();

        }

        /// <summary>
        /// 获取难度系统
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_Diffcult_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Exercise.Diffcult")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取习题的适用范围
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_Scope_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Exercise.Scope")).ToList<ResourceDict>();

        }

        /// <summary>
        /// 获取答题卡的题型
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_CardExerciseType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Exercise.ExerciseAnswercardType")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取试卷的类型
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_PaperType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Paper.PaperType")).ToList<ResourceDict>();
        }



        /// <summary>
        /// 获取资源的文件类型
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_FileType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("File.FileType")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取上传时间的设置
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_TimePass_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("CycleTime")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取浏览权限
        /// </summary>
        /// <returns></returns>
        public static List<ResourceDict> Resource_Dict_ShareRange_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("ShareRange")).ToList<ResourceDict>();

        }


        /// <summary>
        /// 获取知识点掌握程度
        /// </summary>
        /// <returns></returns>
        public static  List<ResourceDict> Resource_Dict_Requirement_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Ken.Requirement")).ToList<ResourceDict>();
        }






    }
}
