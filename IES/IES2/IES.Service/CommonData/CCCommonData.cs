using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.Service.Common;
namespace IES.Service.CommonData
{
    public  class CCCommonData
    {


        /// <summary>
        /// 获取成绩的类型
        /// </summary>
        /// <returns></returns>
        public static List<Dict> Dict_TestScaleType_Get()
        {
            return DictServcie.Resource_Dict_Get().Where( x => x.Source.Equals("Test.ScaleType") ).ToList<Dict>();

        }

        /// <summary>
        /// 获取事务审核类型
        /// </summary>
        /// <returns></returns>
        public static List<Dict> AffairsType_Get()
        {
            return DictServcie.Resource_Dict_Get().Where(x => x.Source.Equals("事务审核")).ToList<Dict>();

        }

        public static List<Dict> Test_ScaleType_Get()
        {
            return DictServcie.Resource_Dict_Get().Where(x => x.Source.Equals("Test.ScaleType")).ToList<Dict>();

        }

        /// <summary>
        /// 获取内容来源
        /// </summary>
        /// <returns></returns>
        public static List<Dict> Dict_Live_Get()
        {
            return DictServcie.Resource_Dict_Get().Where(x => x.Source.Equals("Live.Type")).ToList<Dict>();
        }

        /// <summary>
        /// 获取处理结果
        /// </summary>
        /// <returns></returns>
        public static List<Dict> Dict_Punishment_Get()
        {
            return DictServcie.Resource_Dict_Get().Where(x => x.Source.Equals("Punishment.Type")).ToList<Dict>();
        }
    }
}
