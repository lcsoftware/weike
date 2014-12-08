using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IES.AOP.G2S
{
    /// <summary>
    /// Log4Net 日志服务类 提供对默认配置文件的日志操作
    /// </summary>
    public class Log4NetServer
    {
        private  log4net.ILog _logger = null;
        /// <summary>
        /// ILog
        /// </summary>
        public log4net.ILog ILog
        {
            get { return _logger; }
        }

        internal Log4NetServer(log4net.ILog logger)
        {
            this._logger = logger;
        }
        /// <summary>
        /// 获取一个默认配置文件根 Log4Net 日志类
        /// </summary>
        /// <returns></returns>
        public log4net.ILog getLogger()
        {
            return _logger;
        }

        #region 相关写入日志操作方法
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Debug(object message)
        {
            _logger.Debug(message);
        }
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">日志异常</param>
        public void Debug(object message, Exception exception)
        {
            _logger.Debug(message, exception);
        }
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Error(object message)
        {
            _logger.Error(message);
        }
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">日志异常</param>
        public void Error(object message, Exception exception)
        {
            _logger.Error(message, exception);
        }
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">日志异常</param>
        public void Fatal(object message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Info(object message) 
        {
            _logger.Info(message);
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">日志异常</param>
        public void Info(object message, Exception exception)
        {
            _logger.Info(message, exception);
        }
        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Warn(object message)
        {
            _logger.Warn(message);
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">日志异常</param>
        public void Warn(object message, Exception exception)
        {
            _logger.Warn(message, exception);
        }
        #endregion
    }
}
