/**  版本信息模板在安装目录下，可自行修改。
* Exercise.cs
*
* 功 能： N/A
* 类 名： Exercise
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
namespace IES.Resource.Model
{
    /// <summary>
    /// 习题主表
    /// </summary>
    [Serializable]
    public partial class Exercise : IExercise
    {
        #region 补充字段
        public string ExerciseTypeName { get; set; }
        #endregion

        public Exercise()
        { }
        #region Model
        private int _exerciseid;
        private int _courseid = 0;
        private int _ocid = 0;
        private int _owneruserid = 0;
        private int _createuserid = 0;
        private int _parentid = 0;
        private int _exercisetype;
        private int _diffcult = 1;
        private int _scope = 1;
        private int _sharerange = 3;
        private string _keys;
        private string _kens;
        private string _brief;
        private string _conten;
        private string _answer;
        private string _analysis;
        private string _scorepoint;
        private decimal _score = 0M;
        private bool _isrand = false;
        private DateTime _updatetime = DateTime.Now;
        private bool _isdeleted = false;
        /// <summary>
        /// 主键
        /// </summary>
        public int ExerciseID
        {
            set { _exerciseid = value; }
            get { return _exerciseid; }
        }

        /// <summary>
        /// 习题所属课程编号

        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 在线课程编号
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 资源拥有人编号

        /// </summary>
        public int OwnerUserID
        {
            set { _owneruserid = value; }
            get { return _owneruserid; }
        }
        /// <summary>
        /// 习题创建人

        /// </summary>
        public int CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }

        public string CreateUserName { get; set; }



        /// <summary>
        /// 复合题小题 通过该编号找到题干

        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 1判断题 ; 2单选题 ; 3 多选题 4填空题（客观）5填空题 ; 6连线题 ;7 排序题 ; 8分析题  9计算题   10问答题 ;
        ///11 翻译题  12听力训练  13写作  14阅读理解  15论述题 ;16 答题卡题型  17自定义题型

        /// </summary>
        public int ExerciseType
        {
            set { _exercisetype = value; }
            get { return _exercisetype; }
        }
        /// <summary>
        /// 难度等级 ： 1.简单；2中等；3较难
        /// </summary>
        public int Diffcult
        {
            set { _diffcult = value; }
            get { return _diffcult; }
        }
        /// <summary>
        /// 1  作业与网络考试与随机训练 ；  2 学生自测练习  4  正式考试专用
        /// </summary>
        public int Scope
        {
            set { _scope = value; }
            get { return _scope; }
        }
        /// <summary>
        /// 共享范围：0 :不共享； 1  我教学班学生；   2 本课程师生    ； 3 本学科师生 ；   4全校师生 
        /// </summary>
        public int ShareRange
        {
            set { _sharerange = value; }
            get { return _sharerange; }
        }

        /// <summary>
        /// 关键习题字添加通过其他方法
        /// </summary>
        public string Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }

        /// <summary>
        /// 知识点新增通过其他方法
        /// </summary>
        public string Kens
        {
            set { _kens = value; }
            get { return _kens; }

        }



        /// <summary>
        /// 习题的注释说明

        /// </summary>
        public string Brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
        /// <summary>
        /// 习题内容
        /// </summary>
        public string Conten
        {
            set { _conten = value; }
            get { return _conten; }
        }
        /// <summary>
        /// 习题答案
        /// </summary>
        public string Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 习题解析
        /// </summary>
        public string Analysis
        {
            set { _analysis = value; }
            get { return _analysis; }
        }
        /// <summary>
        /// 主观题得分点
        /// </summary>
        public string ScorePoint
        {
            set { _scorepoint = value; }
            get { return _scorepoint; }
        }
        /// <summary>
        /// 习题的默认分值，对于有多个填空项的习题每个空格分数是该分值除以空格数
        /// </summary>
        public decimal Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 1 选项乱序 ； 0 不乱序

        /// </summary>
        public bool IsRand
        {
            set { _isrand = value; }
            get { return _isrand; }
        }
        /// <summary>
        /// 最后更新时间

        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 删除状态

        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model

    }
}

