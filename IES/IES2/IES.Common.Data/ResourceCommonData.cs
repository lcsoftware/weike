using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IES.Resource.Model;
using IES.G2S.Resource.DAL;

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
        public List<ResourceDict> Resource_Dict_Get()
        {
            return CommonDataDAL.ResourceDict_List();
        }

        /// <summary>
        /// 获取习题的题型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_ExerciseType_Get()
        {
           return  CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("ExerciseType")).ToList<ResourceDict>();

        }

        /// <summary>
        /// 获取难度系统
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_Diffcult_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Diffcult")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取习题的适用范围
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_Scope_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Scope")).ToList<ResourceDict>();

        }

        /// <summary>
        /// 获取答题卡的题型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_CardExerciseType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("ExerciseAnswercardType")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取试卷的类型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_PaperType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("Paper.PaperType")).ToList<ResourceDict>();
        }



        /// <summary>
        /// 获取资源的文件类型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_FileType_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("File.FileType")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取上传时间的设置
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_TimePass_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("CycleTime")).ToList<ResourceDict>();
        }

        /// <summary>
        /// 获取浏览权限
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_ShareRange_Get()
        {
            return CommonDataDAL.ResourceDict_List().Where(x => x.source.Equals("ShareRange")).ToList<ResourceDict>();

        }
    }
}
