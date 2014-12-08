using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.AOP.G2S.Factorys.Cache
{
    internal class HashTableCache : IServerFactoryCache
    {
        private Hashtable _hashtableCache;

        internal HashTableCache()
        {
            _hashtableCache = new Hashtable();
        }

        public void setCache(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException("HashTableCache.setCache key is null");

            if (_hashtableCache.ContainsKey(key))
                _hashtableCache[key] = value;
            else
                _hashtableCache.Add(key, value);
        }

        public object getCache(object key)
        {
            if (key == null)
                throw new ArgumentNullException("HashTableCache.setCache key is null");

            if (_hashtableCache.ContainsKey(key))
                return _hashtableCache[key];
            else
                return null;
        }


        public void clearCache()
        {
            this._hashtableCache.Clear();
        }
    }
}
