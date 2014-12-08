using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys
{
    internal class Log4NetServerFactory : ServerFactoryBase<Log4NetServer>
    {
        public Log4NetServerFactory(IServerFactoryCache cache)
            : base(cache)
        { }

        internal override Log4NetServer GetServer(params object[] arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException("Log4NetServerFactory.GetServer arguments is null");
            if (arguments.Length < 1)
                throw new ArgumentOutOfRangeException("Log4NetServerFactory.GetServer arguments.Length error");

            switch (arguments.Length)
            {
                case 1:
                    return this.getLogServer(arguments[0].ToString());
                case 2:
                    return this.getLogServerAndConfigure(arguments[0].ToString(), arguments[1].ToString());
                default:
                    return null;
            }
        }

        /// 获取默认配置文件中指定loggerName的 Log4NetServer 日志记录类
        private Log4NetServer getLogServer(string loggerName)
        {
            Log4NetServer server = null;
            log4net.ILog log = null;
            string loggerKey = "log4net_" + loggerName;
            server = this.Cache.getCache(loggerKey) as Log4NetServer;
            if (server == null)
            {
                if (loggerName == null || loggerName == "root")
                    log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                else
                    log = log4net.LogManager.GetLogger(loggerName);

                server = new Log4NetServer(log);
                this.Cache.setCache(loggerKey, server);
            }
            return server;
        }
        //设置配置文件路径 获取 logger
        private Log4NetServer getLogServerAndConfigure(string filePath, string loggerName)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(filePath));
            return this.getLogServer(loggerName);
        }
    }
}
