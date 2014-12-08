using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.AOP.G2S.Factorys.Cache;

namespace IES.AOP.G2S.Factorys
{
    /// <summary>
    /// 工厂管理类,提供对AOP服务类的创建
    /// </summary>
    public class FactoryManager
    {
        private IServerFactoryCache _cache;
        private Dictionary<string, object> _factorys;

        internal readonly string NameSpace = "IES.AOP.G2S.Factorys.";
        internal readonly string ClassEnd = "Factory";

        /// <summary>
        /// 缓存
        /// </summary>
        internal IServerFactoryCache Cache
        {
            get { return _cache; }
        }

        public FactoryManager()
            : this(new HashTableCache())
        { }
        internal FactoryManager(IServerFactoryCache cache) 
        {
            this._cache = cache;
            this._factorys = new Dictionary<string, object>();
            this.initServerFactory();
        }
        private void initServerFactory()
        {
            //初始化加载工厂类  可以避免 反射
            this.addServerFactory<ILogServer<IESLogConent>,IESLogServer>(new IESLogServerFactory(this._cache));
            this.addServerFactory<UnityContainerServer>(new UnityContainerServerFactory(this._cache));
        }

        /// <summary>
        /// 获取一个AOP 服务类,返回类型不能带有泛型的类型
        /// </summary>
        /// <typeparam name="TSource">工厂返回类型</typeparam>
        /// <typeparam name="TResult">最终返回类型</typeparam>
        /// <param name="arguments">参数列表</param>
        /// <returns></returns>
        public TResult GetServer<TSource, TResult>(params string[] arguments)
            where TSource : class
            where TResult : class
        {
            string typeName = typeof(TResult).Name;
            ServerFactoryBase<TSource> factoryBase = null;
            //从工厂集合读取
            if (this._factorys.ContainsKey(typeName))
            {
                factoryBase = this._factorys[typeName] as ServerFactoryBase<TSource>;
            }
            //反射加载
            if (factoryBase == null)
            {
                string errorMsg;
                factoryBase = this.createInstanceFactoryObject(this.NameSpace + typeName + this.ClassEnd, out errorMsg) as ServerFactoryBase<TSource>;
                if (factoryBase == null)
                {
                    throw new NullReferenceException("FactoryManager.GetServer factoryBase is null. " + errorMsg);
                }
                this.addServerFactory<TSource,TResult>(factoryBase);
            }
            return factoryBase.GetServer(arguments) as TResult;
        }
        /// <summary>
        ///  获取一个AOP 服务类(只能用于工厂返回类型和最后返回的类型一样的时候)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="arguments">参数列表</param>
        /// <returns></returns>
        public T GetServer<T>(params string[] arguments) where T : class
        {
            return this.GetServer<T, T>(arguments);
        }
        /// <summary>
        /// 添加AOP服务工厂类
        /// </summary>
        /// <typeparam name="TSource">工厂返回类型</typeparam>
        /// <typeparam name="TResult">最终返回类型</typeparam>
        /// <param name="serverFactory">工厂</param>
        internal void addServerFactory<TSource,TResult>(ServerFactoryBase<TSource> serverFactory)
        {
            this._factorys.Add(typeof(TResult).Name, serverFactory);
        }
        /// <summary>
        ///  添加AOP服务工厂类(只能用于工厂返回类型和最后返回的类型一样的时候)
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="serverFactory">工厂</param>
        internal void addServerFactory<T>(ServerFactoryBase<T> serverFactory)
        {
            this.addServerFactory<T, T>(serverFactory);
        }
        /// <summary>
        /// 清空工厂
        /// </summary>
        internal void clearServerFactory()
        {
            this._factorys.Clear();
        }

        /// <summary>
        /// 创建一个实例对象,  值为NULL,说明创建失败
        /// </summary>
        /// <param name="classNameSpace">对象的命名控件</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        internal object createInstanceFactoryObject(string classNameSpace, out string errorMsg)
        {
            object obj = null;
            errorMsg = string.Empty;
            try
            {
                //获取类型
                Type type = Type.GetType(classNameSpace);
                if (type == null)
                {
                    errorMsg = "type is null [" + classNameSpace + "]";
                    return null;
                }
                //创建一个实例
                obj = Activator.CreateInstance(type,this._cache);
            }
            catch (Exception e) { errorMsg = e.Message; }
            return obj;
        }

    }
}
