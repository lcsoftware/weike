using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys
{
    internal abstract class ServerFactoryBase<T> 
    {
        private IServerFactoryCache _cache;
        public IServerFactoryCache Cache
        {
            get { return _cache; }
        }

        public ServerFactoryBase(IServerFactoryCache cache)
        {
            this._cache = cache;
        }

        internal abstract T GetServer(params object[] arguments);
    }
}
