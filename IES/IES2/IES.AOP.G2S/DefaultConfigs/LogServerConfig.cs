using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IES.AOP.G2S.DefaultConfigs
{
    internal static class LogServerConfig
    {
        //设置 Log4Net 默认配置
        internal readonly static string DEFAULT_FilePath = "~/Configs/log4net.config";
        internal readonly static string DEFAULT_LoggerName = "root";

        static LogServerConfig()
        {
            //初始化 默认配置
            if (LogServerConfig.DEFAULT_FilePath != null)
            {
                //路径转换为 WEB 路径
                LogServerConfig.DEFAULT_FilePath = HttpContext.Current.Server.MapPath(LogServerConfig.DEFAULT_FilePath);
            }
        }
    }
}
