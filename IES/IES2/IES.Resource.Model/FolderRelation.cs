/**  版本信息模板在安装目录下，可自行修改。
* Folder.cs
*
* 功 能： N/A
* 类 名： Folder
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:49   N/A    初版
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
    public enum FileType
    {
        Folder,
        File
    }

    public class FolderRelation
    {
        public FolderRelation()
        {
            this.Children = new List<FolderRelation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public FileType RelationType { get; set; }

        public int OCID { get; set; }

        public int CourseId { get; set; }

        public int ParentID { get; set; }

        public IList<FolderRelation> Children { get; set; }

        public FolderRelation Parent { get; set; }

    }
}
