
/**  版本信息模板在安装目录下，可自行修改。
* Chapter.cs
*
* 功 能： N/A
* 类 名： Chapter
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:45   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    public class ResourceKen
    {
        public int ID { get; set; }
        public int ResourceID { get; set; }
        public int KenID { get; set; }
        public string Source { get; set; }

    }
}
