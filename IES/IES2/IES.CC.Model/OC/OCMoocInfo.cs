using IES.CC.OC.Model;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IES.CC.OC.Model
{
    public class OCMoocInfo
    {
        /// <summary>
        /// MOOC基本信息
        /// </summary>
        public OCMooc OcMooc { get; set; }

        /// <summary>
        /// MOOC章节集合
        /// </summary>
        public List<Chapter> ChapterList { get; set; }
    }
}
