using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace IES.AOP.G2S
{
    /// <summary>
    /// UnityContainer帮助类、从配置文件读取
    /// </summary>
    public class UnityContainerHelp
    {
        private static UnityContainerHelp _staticContainerHelp;
        public static UnityContainerHelp ContainerHelp
        {
            get { return _staticContainerHelp; }
        }
        static UnityContainerHelp()
        {
            UnityContainerHelp._staticContainerHelp = new UnityContainerHelp("unity", "FirstClass");
        }



        private IUnityContainer _container;

        public UnityContainerHelp(string unitySection, string containerName)
        {
            _container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(unitySection);
            _container.LoadConfiguration(section, containerName);
        }

        public UnityContainerHelp(string xmlFile, string unitySection, string containerName)
        {
            _container = new UnityContainer();
            var configMap = new ConfigurationFileMap(xmlFile);
            var configuration = ConfigurationManager.OpenMappedMachineConfiguration(configMap);
            UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(unitySection);
            _container.LoadConfiguration(section, containerName);
        }


        public T getServer<T>()
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ConfigName">配置文件中指定的文字</param>
        /// <returns></returns>
        public T getServer<T>(string ConfigName)
        {
            return _container.Resolve<T>(ConfigName);
        }

        /// <summary>
        /// 返回构结函数带参数
        /// </summary>
        /// <typeparam name="T">依赖对象</typeparam>
        /// <param name="parameterList">参数集合（参数名，参数值）</param>
        /// <returns></returns>
        public T getServer<T>(Dictionary<string, object> parameterList)
        {
            var list = new ParameterOverrides();
            foreach (KeyValuePair<string, object> item in parameterList)
            {
                list.Add(item.Key, item.Value);
            }
            return _container.Resolve<T>(list);
        }
        /// <summary>
        /// 返回构结函数带参数
        /// </summary>
        /// <typeparam name="T">依赖对象</typeparam>
        /// <param name="ConfigName">配置文件中指定的文字(没写会报异常)</param>
        /// <param name="parameterList">参数集合（参数名，参数值）</param>
        /// <returns></returns>
        public T getServer<T>(string ConfigName, Dictionary<string, object> parameterList)
        {
            var list = new ParameterOverrides();
            foreach (KeyValuePair<string, object> item in parameterList)
            {
                list.Add(item.Key, item.Value);
            }
            return _container.Resolve<T>(ConfigName, list);
        }



        #region 默认静态方法
        public static T GetServer<T>()
        {
            return _staticContainerHelp.getServer<T>();
        }
        public static T GetServer<T>(string ConfigName)
        {
            return _staticContainerHelp.getServer<T>(ConfigName);
        }
        public static T GetServer<T>(Dictionary<string, object> parameterList)
        {
            return _staticContainerHelp.getServer<T>(parameterList);
        }
        public T GetServer<T>(string ConfigName, Dictionary<string, object> parameterList)
        {
            return _staticContainerHelp.getServer<T>(ConfigName, parameterList);
        }
        #endregion
    }
}
