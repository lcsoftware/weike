using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IES.AOP.G2S.DefaultConfigs
{
    internal static class Log4NetServerConfig
    {
        //设置 Log4Net 默认配置
        internal readonly static string DEFAULT_FilePath = "~/Configs/log4net.config";
        internal readonly static string DEFAULT_LoggerName = "root";

        static Log4NetServerConfig()
        {
            //初始化 默认配置
            if (Log4NetServerConfig.DEFAULT_FilePath == null)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            else
            {
                //路径转换为 WEB 路径
                Log4NetServerConfig.DEFAULT_FilePath = HttpContext.Current.Server.MapPath(Log4NetServerConfig.DEFAULT_FilePath);
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Log4NetServerConfig.DEFAULT_FilePath));
            }
        }
    }
}
