using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IES.AOP.G2S.DefaultConfigs
{
    internal static class UnityContainerServerConfig
    {
        //设置 UnityContainer 默认配置
        internal readonly static string DEFAULT_FilePath = "~/Configs/exerciseaop.xml";
        internal readonly static string DEFAULT_Section = "unity";
        internal readonly static string DEFAULT_ContainerName = "FirstClass";

        static UnityContainerServerConfig()
        {
            //初始化 默认配置
            if (UnityContainerServerConfig.DEFAULT_FilePath != null)
                UnityContainerServerConfig.DEFAULT_FilePath = HttpContext.Current.Server.MapPath(UnityContainerServerConfig.DEFAULT_FilePath);
        }
    }
}
