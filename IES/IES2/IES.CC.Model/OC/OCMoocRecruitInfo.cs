using IES.CC.OC.Model;
using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IES.CC.Model.OC
{
    public class OCMoocRecruitInfo
    {
        /// <summary>
        /// 教学互动基本信息
        /// </summary>
        public OCMoocRecruit OCMoocRecruit { get; set; }

        /// <summary>
        /// 教学班集合
        /// </summary>
        public List<OCClass> OCClassList { get; set; }
    }
}
