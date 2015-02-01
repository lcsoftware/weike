/**  版本信息模板在安装目录下，可自行修改。
* TestUser.cs
*
* 功 能： N/A
* 类 名： TestUser
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:23:48   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Test.Model
{
	/// <summary>
	/// 测试学生结果表
	/// </summary>
	[Serializable]
	public partial class TestUser
    {
        #region  补充信息

        /// <summary>
        /// 专业名称
        /// </summary>
        public string SpecialtyName { get; set; }

        /// <summary>
        /// 行政班班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 测试名称
        /// </summary>
        public string TestName { get; set; }


        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string OrganizationName { get; set; }


        /// <summary>
        /// 教学班名称
        /// </summary>
        public string TeachingClassName { get; set; }


        #endregion


        public TestUser()
		{}
		#region Model
		private int _testuserid;
		private int _testid;
		private int _userid;
		private int _checkuserid=0;
		private int _status;
		private bool _issample= false;
		private decimal? _objectivescore;
		private string _fastscore;
		private decimal? _subjectivescore;
		private DateTime? _submittime;
		private string _comment;
		private int _scoretype;
		private int? _checkhistory=1;
		/// <summary>
		/// 
		/// </summary>
		public int TestUserID
		{
			set{ _testuserid=value;}
			get{return _testuserid;}
		}
		/// <summary>
		///  测试编号
		/// </summary>
		public int TestID
		{
			set{ _testid=value;}
			get{return _testid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}

        public string UserName { get; set; }
		/// <summary>
		/// 流水批阅中批阅教师编号
       
        ///学号
        public string UserNo { get; set; }
		/// </summary>
		public int CheckUserID
		{
			set{ _checkuserid=value;}
			get{return _checkuserid;}
		}

        /// <summary>
        /// 测试完成百分比
        /// </summary>
        public int FinishPer { get; set; }



		/// <summary>
		/// 1.未完成,, 10 暂存 11 退回,  20 已提交  23 批阅完成  30 已发放
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}

        /// <summary>
        /// 是否迟交
        /// </summary>
        public int IsDelay { get; set; }

		/// <summary>
		/// 是否作为作业范本
		/// </summary>
		public bool IsSample
		{
			set{ _issample=value;}
			get{return _issample;}
		}
		/// <summary>
		/// 客观题得分
		/// </summary>
		public decimal? ObjectiveScore
		{
			set{ _objectivescore=value;}
			get{return _objectivescore;}
		}
		/// <summary>
		/// 快速设分
		/// </summary>
		public string FastScore
		{
			set{ _fastscore=value;}
			get{return _fastscore;}
		}
		/// <summary>
		/// 主观题得分
		/// </summary>
		public decimal? SubjectiveScore
		{
			set{ _subjectivescore=value;}
			get{return _subjectivescore;}
		}

        /// <summary>
        /// 测试 学生互评最后成绩，互评截止时间过后统计生成
        /// </summary>
        public string StudentCheckScore { get; set; }

        /// <summary>
        /// 成绩分布段
        /// </summary>
        public string SocreSection { get; set; }
		/// <summary>
		/// 提交时间
		/// </summary>
		public DateTime? SubmitTime
		{
			set{ _submittime=value;}
			get{return _submittime;}
		}
		/// <summary>
		/// 评语
		/// </summary>
		public string Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		/// <summary>
		/// 1.标准百分制
        ///2.通过、不通过
        ///3.等级制（中文） 不及格、及格、良好、优秀
        ///4.等级制（英文） E  D  C  B  A
		/// </summary>
		public int ScoreType
		{
			set{ _scoretype=value;}
			get{return _scoretype;}
		}
		/// <summary>
		/// 1正常；2重做 ； 4迟交
		/// </summary>
		public int? CheckHistory
		{
			set{ _checkhistory=value;}
			get{return _checkhistory;}
		}
		#endregion Model

	}
}

