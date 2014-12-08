using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S
{
    /// <summary>
    /// 提供对IES.G2S相关操作记录的日志类
    /// </summary>
    public class IESLogServer:ILogServer<IESLogConent>
    {
        private  log4net.ILog _logger = null;
        /// <summary>
        /// log4net.ILog 
        /// </summary>
        public log4net.ILog ILog
        {
            get { return _logger; }
        }
        public IESLogServer(log4net.ILog logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Debug(IESLogConent message)
        {
            _logger.Debug(message);
        }
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        public void Debug(IESLogConent message, Exception exception)
        {
            _logger.Debug(message, exception);
        }
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public void Debug(int userID, int courseID, int ocID, int moduleID, int actionID, string ip,string conten)
        {
            this.Debug(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip,conten));
        }
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">日志异常</param>
        public void Debug(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten, Exception exception)
        {
            this.Debug(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten), exception);
        }

        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Error(IESLogConent message)
        {
            _logger.Error(message);
        }
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        public void Error(IESLogConent message, Exception exception)
        {
            _logger.Error(message, exception);
        }
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public void Error(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten)
        {
            this.Error(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten));
        }
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">日志异常</param>
        public void Error(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten, Exception exception)
        {
            this.Error(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten), exception);
        }


        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Fatal(IESLogConent message)
        {
            _logger.Fatal(message);
        }
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        public void Fatal(IESLogConent message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public void Fatal(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten)
        {
            this.Fatal(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten));
        }
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">日志异常</param>
        public void Fatal(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten, Exception exception)
        {
            this.Fatal(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten), exception);
        }

        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Info(IESLogConent message)
        {
            _logger.Info(message);
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        public void Info(IESLogConent message, Exception exception)
        {
            _logger.Info(message, exception);
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public void Info(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten)
        {
            this.Info(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten));
        }
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">日志异常</param>
        public void Info(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten, Exception exception)
        {
            this.Info(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten), exception);
        }

        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Warn(IESLogConent message)
        {
            _logger.Warn(message);
        }
        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        public void Warn(IESLogConent message, Exception exception)
        {
            _logger.Warn(message, exception);
        }
        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">conten</param>
        public void Warn(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten)
        {
            this.Info(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten));
        }
        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="courseID">courseID</param>
        /// <param name="ocID">ocID</param>
        /// <param name="moduleID">moduleID</param>
        /// <param name="actionID">actionID</param>
        /// <param name="ip">ip</param>
        /// <param name="conten">日志异常</param>
        public void Warn(int userID, int courseID, int ocID, int moduleID, int actionID, string ip, string conten, Exception exception)
        {
            this.Info(new IESLogConent(userID, courseID, ocID, moduleID, actionID, ip, conten), exception);
        }
    }
}
