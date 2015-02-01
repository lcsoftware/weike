using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.JW.Model;
using IES.Cache;
using IES.CommonDAL;

namespace IES.Service.Common
{
    public   class DictServcie
    {
        /// <summary>
        /// 获取所有的字典列表
        /// </summary>
        /// <returns></returns>
        public static List<Dict> Resource_Dict_Get()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "Dict"))
            {
                List<Dict> dictlist = IES.CommonDAL.CommonDAL.Dict_List();
                cache.Set(string.Empty, "Dict", dictlist);
                return dictlist;
            }
            else
            {
                return cache.Get<List<Dict>>(string.Empty, "Dict");
            }
        }

    }
}
