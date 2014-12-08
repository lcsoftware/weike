using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S
{
    /// <summary>
    /// 提供记录不同级别日志方法的接口
    /// </summary>
    /// <typeparam name="T">记录的日志内容类型</typeparam>
    public interface ILogServer<T>
    {
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Debug(T message);
        /// <summary>
        /// 记录调试级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        void Debug(T message, Exception exception);
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Error(T message);
        /// <summary>
        /// 记录普通错误级别的日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
         void Error(T message, Exception exception);
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
         /// <param name="message">日志内容</param>
         void Fatal(T message);
        /// <summary>
        /// 记录严重性错误级别的日志
        /// </summary>
         /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        void Fatal(T message, Exception exception);
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Info(T message);
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
        void Info(T message, Exception exception);
        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="message">日志内容</param>
         void Warn(T message);
        /// <summary>
        /// 记录普通信息级别日志
        /// </summary>
         /// <param name="message">日志内容</param>
        /// <param name="exception">日志异常</param>
         void Warn(T message, Exception exception);

    }
}
