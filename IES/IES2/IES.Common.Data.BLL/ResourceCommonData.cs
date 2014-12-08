using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Common.Data.IBLL ;
using IES.Resource.Model;

namespace IES.Common.Data.BLL
{
    public class ResourceCommonData : IResourceCommonData
    {
        /// <summary>
        /// 获取所有的资源库下的字典表
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_Get()
        {
            return new List<ResourceDict>();
            
        }



        /// <summary>
        /// 获取习题的题型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_ExerciseType_Get()
        {
            return new List<ResourceDict>();

        }

        /// <summary>
        /// 获取难度系统
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_Diffcult_Get()
        {
            return new List<ResourceDict>();

        }

        /// <summary>
        /// 获取习题的适用范围
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_Scope_Get()
        {
            return new List<ResourceDict>();

        }


        /// <summary>
        /// 获取答题卡的题型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_CardExerciseType_Get()
        {
            return new List<ResourceDict>();

        }


        /// <summary>
        /// 获取试卷的类型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_PaperType_Get()
        {
            return new List<ResourceDict>();

        }




        /// <summary>
        /// 获取资源的文件类型
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_FileType_Get()
        {
            return new List<ResourceDict>();

        }

        /// <summary>
        /// 获取上传时间的设置
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_TimePass_Get()
        {
            return new List<ResourceDict>();

        }


        /// <summary>
        /// 获取浏览权限
        /// </summary>
        /// <returns></returns>
        public List<ResourceDict> Resource_Dict_ViewAu_Get()
        {
            return new List<ResourceDict>();

        }

    }
}
