using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IES.AOP.G2S.DefaultConfigs
{
    /// <summary>
    /// jwaop.config 对应的配置
    /// </summary>
    internal static class JwServerConfig
    {
        //设置 UnityContainer 默认配置
        internal readonly static string DEFAULT_FilePath = "~/Configs/jwaop.config";
        internal readonly static string DEFAULT_Section = "unity";
        internal readonly static string DEFAULT_ContainerName = "FirstClass";

        static JwServerConfig()
        {
            //初始化 默认配置
            if (JwServerConfig.DEFAULT_FilePath != null)
                JwServerConfig.DEFAULT_FilePath = HttpContext.Current.Server.MapPath(JwServerConfig.DEFAULT_FilePath);
        }
    }
}
