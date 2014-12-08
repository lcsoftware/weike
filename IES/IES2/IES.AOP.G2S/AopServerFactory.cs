using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IES.AOP.G2S.Factorys;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using IES.AOP.G2S.DefaultConfigs;


namespace IES.AOP.G2S
{
    /// <summary>
    /// 提供对AopServer快速创建的工厂类, 如果需要创建更具体的服务类,请使用IES.AOP.G2S.Factorys.FactoryManager
    /// </summary>
    public static class AopServerFactory
    {
        private readonly static FactoryManager _factoryManager = new FactoryManager();

        /// <summary>
        /// 清理缓存
        /// </summary>
        public static void clearCache()
        {
            _factoryManager.Cache.clearCache();
        }
        /// <summary>
        /// 清理工厂
        /// </summary>
        public static void clearFactory()
        {
            _factoryManager.clearServerFactory();
        }


        #region 创建 指定的 UnityContainerServer服务类方法 region

        /// <summary>
        /// 获取一个由指定文件配置的指定UnityContainerServer类 
        /// </summary>
        /// <param name="xmlFile">配置文件完整物理路径</param>
        /// <param name="unitySection">unitySection节点名称</param>
        /// <param name="containerName">配置文件指定的containerName</param>
        /// <returns></returns>
        public static UnityContainerServer getContainerServer(string xmlFile, string unitySection, string containerName)
        {
            return _factoryManager.GetServer<UnityContainerServer>(xmlFile, unitySection, containerName);
        }

        #endregion

        #region 创建 ExerciseAop 相关UnityContainerServer服务类方法 region

        /// <summary>
        /// 获取一个由 exerciseaop.config 配置的 默认 ExerciseAop  UnityContainerServer类 
        /// </summary>
        /// <returns></returns>
        public static UnityContainerServer getExerciseServer()
        {
            return _factoryManager.GetServer<UnityContainerServer>(ExerciseServerConfig.DEFAULT_FilePath, ExerciseServerConfig.DEFAULT_Section, ExerciseServerConfig.DEFAULT_ContainerName);
        }
        /// <summary>
        /// 获取一个由 exerciseaop.config 配置的 指定 ExerciseAop  UnityContainerServer类 
        /// </summary>
        /// <param name="containerName">配置文件指定的containerName</param>
        /// <returns></returns>
        public static UnityContainerServer getExerciseServer(string containerName)
        {
            return _factoryManager.GetServer<UnityContainerServer>(ExerciseServerConfig.DEFAULT_FilePath, ExerciseServerConfig.DEFAULT_Section, containerName);
        }

        #endregion

        #region 创建 JwAop 相关UnityContainerServer服务类方法 region

        /// <summary>
        /// 获取一个由 jwaop.config 配置的 默认 JwAop  UnityContainerServer类 
        /// </summary>
        /// <returns></returns>
        public static UnityContainerServer getJwServer()
        {
            return _factoryManager.GetServer<UnityContainerServer>(JwServerConfig.DEFAULT_FilePath, JwServerConfig.DEFAULT_Section, JwServerConfig.DEFAULT_ContainerName);
        }
        /// <summary>
        ///获取一个由 jwaop.config 配置的 指定 JwAop  UnityContainerServer类 
        /// </summary>
        /// <param name="containerName">配置文件指定的containerName</param>
        /// <returns></returns>
        public static UnityContainerServer getJwServer(string containerName)
        {
            return _factoryManager.GetServer<UnityContainerServer>(JwServerConfig.DEFAULT_FilePath, JwServerConfig.DEFAULT_Section, containerName);
        }

        #endregion

        #region 创建 IESLogServer 服务类方法 region

        /// <summary>
        /// 获取一个由 log4net.config 配置的 默认  IESLogServer记录日志类
        /// </summary>
        /// <returns></returns>
        public static IESLogServer getLogServer()
        {
            return _factoryManager.GetServer<ILogServer<IESLogConent>, IESLogServer>(LogServerConfig.DEFAULT_FilePath, LogServerConfig.DEFAULT_LoggerName);
        }
        /// <summary>
        ///  获取一个由 log4net.config 配置的 指定  IESLogServer记录日志类
        /// </summary>
        /// <param name="loggerName">配置文件名的loggerName</param>
        /// <returns></returns>
        public static IESLogServer getLogServer(string loggerName)
        {
            return _factoryManager.GetServer<ILogServer<IESLogConent>, IESLogServer>(LogServerConfig.DEFAULT_FilePath, loggerName);
        }

        /// <summary>
        ///  获取一个由 指定 配置的 指定 IESLogServer记录日志类
        /// </summary>
        /// <param name="filePath">配置文件物理路径</param>
        /// <param name="loggerName">loggerName</param>
        /// <returns></returns>
        public static IESLogServer getLogServer(string filePath, string loggerName)
        {
            return _factoryManager.GetServer<ILogServer<IESLogConent>, IESLogServer>(filePath, loggerName);
        }

        #endregion
    }

}
