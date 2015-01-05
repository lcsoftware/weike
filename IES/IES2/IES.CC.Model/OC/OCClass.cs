/**  版本信息模板在安装目录下，可自行修改。
* OCClass.cs
*
* 功 能： N/A
* 类 名： OCClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:22   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.OC.Model
{
    /// <summary>
    /// 我建设的在线课程
    /// </summary>
    [Serializable]
    public partial class OCClass
    {
        #region 补充信息

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        #endregion

        public OCClass()
        { }
        #region Model
        private int _occlassid;
        private int _ocid;
        private int _teachingclassid;

        /// <summary>
        /// 
        /// </summary>
        public int OCClassID
        {
            set { _occlassid = value; }
            get { return _occlassid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }

        public int JoinType { get; set; }


        public DateTime RecruitStartDate { get; set; }

        public DateTime RecruitEndDate { get; set; }


        public int UserLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TeachingClassID
        {
            set { _teachingclassid = value; }
            get { return _teachingclassid; }
        }


        public string TeachingClassName { get; set; }


        public int RegNum { get; set; }


        public bool RegStatus { get; set; }


        public bool IsHistroy { get; set; }

        public int UserID { get; set; }

        public DateTime CreateTime { get; set; }
        public int ClassType { get; set; }

        public bool RecruitStatus { get; set; }


        #endregion Model

    }
}

