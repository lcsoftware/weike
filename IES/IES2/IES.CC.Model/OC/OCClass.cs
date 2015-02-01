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
        public string  Key { get; set; }
        /// <summary>
        /// 教学运行开始结束时间
        /// </summary>
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 授课教师
        /// </summary>
        public string Teacherinfo { get; set; }
        /// <summary>
        /// 其他教学团队
        /// </summary>
        public string Teaminfo { get; set; }
        /// <summary>
        /// 学生数量
        /// </summary>
        public int StudentCount { get; set; }
        public int LineStudent { get; set; }
        public int OffLineStudent { get; set; }
        /// <summary>
        /// 授课教师编号
        /// </summary>
        public int TeacherID { get; set; }
        public string TeacherIDS { get; set;}
        /// <summary>
        /// 教学班学生集合 ，分割
        /// </summary>
        public string StudentIDS { get; set; }

        public int TermID { get; set; }

        public int RowsCount { get; set; }

        // 班级平均进度
        public int AvgProgress { get; set; }

        //高于计划人数
        public int HighThanPlan { get; set; }

        //低于计划人数
        public int LowThanPlan { get; set; }

        //没有学习人数
        public int NoStudy { get; set; }

        #endregion

        public OCClass()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        public int OCClassID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OCID { get; set; }


        public int JoinType { get; set; }

        /// <summary>
        /// 注册开始结束时间  招生起止日期
        /// </summary>
        public DateTime RecruitStartDate { get; set; }

        public DateTime RecruitEndDate { get; set; }

        public int UserLimit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TeachingClassID { get; set; }

        public string TeachingClassName { get; set; }


        public string RegNum { get; set; }


        public bool RegStatus { get; set; }

        public bool IsHistroy { get; set; }

        public DateTime CreateTime { get; set; }
        public int ClassType { get; set; }

        public bool RecruitStatus { get; set; }

        #endregion Model

    }
}

