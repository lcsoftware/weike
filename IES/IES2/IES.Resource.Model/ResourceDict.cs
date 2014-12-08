using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{

    /// <summary>
    /// 资源库的字典表
    /// </summary>
    public class ResourceDict
    {

        public string id { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string nameen { get; set; }

        public string source { get; set; }
    }
}
