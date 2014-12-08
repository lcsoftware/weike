using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys
{
    internal interface IServerFactoryCache
    {
        void setCache(object key, object value);
        object getCache(object key);
        void clearCache();
    }
}
