/**  版本信息模板在安装目录下，可自行修改。
* Surveyobject.cs
*
* 功 能： N/A
* 类 名： Surveyobject
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/1/5 18:49:10   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.Survey
{
	/// <summary>
	/// 问卷发放对象
	/// </summary>
	[Serializable]
	public partial class Surveyobject
	{
		public Surveyobject()
		{}
		#region Model
		private int _surveyobjectid;
		private int _surveyid;
		private string _source;
		private int _sourceid;
		/// <summary>
		/// 
		/// </summary>
		public int SurveyobjectID
		{
			set{ _surveyobjectid=value;}
			get{return _surveyobjectid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SurveyID
		{
			set{ _surveyid=value;}
			get{return _surveyid;}
		}
		/// <summary>
		/// TechingClass ;  Class ;  User ; 
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 教学班编号 2.行政班编号  3.学生编号
		/// </summary>
		public int SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		#endregion Model

	}
}

