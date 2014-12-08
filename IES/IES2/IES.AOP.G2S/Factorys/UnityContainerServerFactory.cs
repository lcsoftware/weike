using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace IES.AOP.G2S.Factorys
{
    internal class UnityContainerServerFactory : ServerFactoryBase<UnityContainerServer>
    {
        public UnityContainerServerFactory(IServerFactoryCache cache)
            :base(cache)
        { }

        internal override UnityContainerServer GetServer(params object[] arguments)
        {
            if (arguments == null)
                throw new ArgumentNullException("UnityContainerServerFactory.GetServer arguments is null");
            if(arguments.Length != 3)
                throw new ArgumentOutOfRangeException("UnityContainerServerFactory.GetServer arguments.Length error");

            return this.getUnityServer(arguments[0].ToString(), arguments[1].ToString(), arguments[2].ToString());
        }

        //获取指定一个服务对象
        private UnityContainerServer getUnityServer(string xmlFile, string unitySectionName, string containerName)
        {
            UnityContainerServer server = null;
            string cachekey_server = string.Format("{0}_{1}_{2}", xmlFile, unitySectionName, containerName);
            server = this.Cache.getCache(cachekey_server) as UnityContainerServer;
            if (server == null)
            {
                IUnityContainer unityContainer = new UnityContainer();
                unityContainer.LoadConfiguration(getUnitySection(xmlFile, unitySectionName), containerName);
                server = new UnityContainerServer(unityContainer);
                this.Cache.setCache(cachekey_server, server);
            }
            return server;
        }
        //获取指定的UnitySection节点
        private UnityConfigurationSection getUnitySection(string xmlFile, string unitySectionName)
        {
            UnityConfigurationSection section = null;
            string cachekey_section = string.Format("{0}_{1}", xmlFile, unitySectionName);
            section = this.Cache.getCache(cachekey_section) as UnityConfigurationSection;
            if (section == null)
            {
                if (xmlFile == null)
                    section = (UnityConfigurationSection)ConfigurationManager.GetSection(unitySectionName);
                else
                    section = (UnityConfigurationSection)getConfiguration(xmlFile).GetSection(unitySectionName);
                this.Cache.setCache(cachekey_section, section);
            }
            return section;
        }
        //从指定路径中加载配置文件
        private  Configuration getConfiguration(string xmlFile)
        {
            return ConfigurationManager.OpenMappedMachineConfiguration(new ConfigurationFileMap(xmlFile)); 
        }

    }
}
