using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys
{
    internal abstract class LogServerFactory<TSource, TResult> : ServerFactoryBase<TSource>
        where TSource : class
        where TResult : class
    {
        internal readonly string LOG4NETKEY_CONFIGFILE = "log4net_configfile";
        internal readonly string LOG4NETKEY_CONFIGFILE_DEFAULT = "log4net_configfile_default";

        public LogServerFactory(IServerFactoryCache cache)
            : base(cache)
        { }

        internal override TSource GetServer(params object[] arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException("Log4NetServerFactory.GetServer arguments is null");
            if (arguments.Length < 1)
                throw new ArgumentOutOfRangeException("Log4NetServerFactory.GetServer arguments.Length error");

            switch (arguments.Length)
            {
                case 1:
                    return this.getLogServerAndConfigure(LOG4NETKEY_CONFIGFILE_DEFAULT, arguments[0].ToString()) as TSource;
                case 2:
                    return this.getLogServerAndConfigure(arguments[0].ToString(), arguments[1].ToString()) as TSource;
                default:
                    return null;
            }
        }

        /// 获取默认配置文件中指定loggerName的 Log4NetServer 日志记录类
        protected virtual TResult getLogServer(string loggerName)
        {
            TResult server = null;
            log4net.ILog logger = null;
            string loggerKey = "log4net_" + typeof(TResult).Name + "_" + loggerName;
            server = (TResult)this.Cache.getCache(loggerKey);
            if (server == null)
            {
                if (loggerName == null || loggerName == "root")
                    logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                else
                    logger = log4net.LogManager.GetLogger(loggerName);

                if (logger == null)
                    throw new NullReferenceException("LogServerFactory.getLogServer log4net.ILog log is null");

                server = Activator.CreateInstance(typeof(TResult), logger) as TResult;
                this.Cache.setCache(loggerKey, server);
            }
            return server;
        }
        //设置配置文件路径 获取 logger
        protected virtual TResult getLogServerAndConfigure(string filePath, string loggerName)
        {
            string currentConfigPath = this.Cache.getCache(LOG4NETKEY_CONFIGFILE) as string;
            //初始化 默认配置
            if (currentConfigPath != filePath)
            {
                if (filePath == LOG4NETKEY_CONFIGFILE_DEFAULT)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    this.Cache.setCache(LOG4NETKEY_CONFIGFILE, LOG4NETKEY_CONFIGFILE_DEFAULT);
                }
                else
                {
                    //路径转换为 WEB 路径
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(filePath));
                    this.Cache.setCache(LOG4NETKEY_CONFIGFILE, filePath);
                }
            }
            return this.getLogServer(loggerName);
        }
    }
}
