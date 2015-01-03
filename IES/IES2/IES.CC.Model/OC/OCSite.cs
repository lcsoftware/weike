/**  版本信息模板在安装目录下，可自行修改。
* OCSite.cs
*
* 功 能： N/A
* 类 名： OCSite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:29   N/A    初版
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
	/// 课程网站建设模式主表
	/// </summary>
	[Serializable]
	public partial class OCSite
	{
		public OCSite()
		{}
		#region Model
		private int _siteid;
		private int _ocid;
		private int _displaystyle;
		private bool _buildmode= true;
		private string _outsitelink;
		private int? _templateid;
		private string _brief;
		private bool _useindexpage= true;
		private bool _useresource= true;
		private bool _uselive= true;
		private bool _usemoocplan= false;
		private bool _isdeleted= false;
        private int _language;
		/// <summary>
		/// 
		/// </summary>
		public int SiteID
		{
			set{ _siteid=value;}
			get{return _siteid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCID
		{
			set{ _ocid=value;}
			get{return _ocid;}
		}
		/// <summary>
		/// 1 ,2,3 页面表现风格
		/// </summary>
		public int DisplayStyle
		{
			set{ _displaystyle=value;}
			get{return _displaystyle;}
		}
        /// <summary>
        /// 语言 1为中文 0为英文
        /// </summary>
        public int Language {
            set { _language = value; }
            get { return _language; }
        }
		/// <summary>
		/// 建设模式 ： 1 利用平台， 0外部链接
		/// </summary>
		public bool BuildMode
		{
			set{ _buildmode=value;}
			get{return _buildmode;}
		}

		/// <summary>
		/// 外部链接地址
		/// </summary>
		public string OutSiteLink
		{
			set{ _outsitelink=value;}
			get{return _outsitelink;}
		}
		/// <summary>
		/// 模板编号
		/// </summary>
		public int? TemplateID
		{
			set{ _templateid=value;}
			get{return _templateid;}
		}
		/// <summary>
		/// 课程推荐词
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 是否启用首页
		/// </summary>
		public bool UseIndexPage
		{
			set{ _useindexpage=value;}
			get{return _useindexpage;}
		}
		/// <summary>
		/// 是否开启教学资料
		/// </summary>
		public bool UseResource
		{
			set{ _useresource=value;}
			get{return _useresource;}
		}
		/// <summary>
		/// 是否启动课程互动
		/// </summary>
		public bool UseLive
		{
			set{ _uselive=value;}
			get{return _uselive;}
		}
		/// <summary>
		/// 启动MOOC 教学计划
		/// </summary>
		public bool UseMoocPlan
		{
			set{ _usemoocplan=value;}
			get{return _usemoocplan;}
		}
		/// <summary>
		/// 1 删除  ； 0 未删除
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

