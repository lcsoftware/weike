using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Collections;
using System.Web;

namespace IES.AOP.G2S
{

    /// <summary>
    /// UnityContainer服务类
    /// </summary>
    public class UnityContainerServer
    {
        private IUnityContainer _container;
        public IUnityContainer UnityContainer
        {
            get{return _container;}
        }
        public UnityContainerServer(IUnityContainer container)
        {
            this._container = container;
        }
        /// <summary>
        /// 返回一个无参构造的服务对象
        /// </summary>
        /// <typeparam name="T">注册的对象类型</typeparam>
        /// <returns></returns>
        public T GetServer<T>()
        {
            return _container.Resolve<T>();
        }
        /// <summary>
        /// 返回一个注册了名字的无参构造的服务对象
        /// </summary>
        /// <typeparam name="T">注册的对象类型</typeparam>
        /// <param name="name">注册的名字</param>
        /// <returns></returns>
        public T GetServer<T>(string name)
        {
            return _container.Resolve<T>(name);
        }

        /// <summary>
        ///  返回一个有参构造的服务对象
        /// </summary>
        /// <typeparam name="T">注册的对象类型</typeparam>
        /// <param name="parameterList">参数集合（参数名，参数值）</param>
        /// <returns></returns>
        public T GetServer<T>(Dictionary<string, object> parameterList)
        {
            var list = new ParameterOverrides();
            foreach (KeyValuePair<string, object> item in parameterList)
            {
                list.Add(item.Key, item.Value);
            }
            return _container.Resolve<T>(list);
        }
        /// <summary>
        /// 返回一个注册了名字的有参构造的服务对象
        /// </summary>
        /// <typeparam name="T">注册的对象类型</typeparam>
        /// <param name="name">配置文件中指定的文字(没写会报异常)</param>
        /// <param name="parameterList">参数集合（参数名，参数值）</param>
        /// <returns></returns>
        public T GetServer<T>(string name, Dictionary<string, object> parameterList)
        {
            var list = new ParameterOverrides();
            foreach (KeyValuePair<string, object> item in parameterList)
            {
                list.Add(item.Key, item.Value);
            }
            return _container.Resolve<T>(name, list);
        }
    }
}
