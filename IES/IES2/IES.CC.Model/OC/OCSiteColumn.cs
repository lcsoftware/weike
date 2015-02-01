/**  版本信息模板在安装目录下，可自行修改。
* OCSiteColumn.cs
*
* 功 能： N/A
* 类 名： OCSiteColumn
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:30   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace IES.CC.OC.Model
{
	/// <summary>
	/// 课程网站建设栏目
	/// </summary>
	[Serializable]
	public partial class OCSiteColumn
	{
		public OCSiteColumn()
		{
            Children = new List<OCSiteColumn>();
        }
        #region 补充字段
        private bool _useindexpage;
        private bool _useresource;
        private bool _uselive;
        private bool _useMoocplan;
        private bool _isshow;
        /// <summary>
        /// 首页是否展示
        /// </summary>
        public bool UseIndexPage
        {
            set { _useindexpage = value; }
            get { return _useindexpage; }
        }
        /// <summary>
        /// 课程资源 是否显示
        /// </summary>
        public bool UseResource
        {
            set { _useresource = value; }
            get { return _useresource; }
        }
        /// <summary>
        /// 课程互动是否显示
        /// </summary>
        public bool UseLive {
            set { _uselive = value; }
            get { return _uselive; }  
        }
        /// <summary>
        /// mooc 是否展示
        /// </summary>
        public bool UseMoocPlan
        {
            set { _useMoocplan = value; }
            get { return _useMoocplan; }  
        }
        /// <summary>
        /// 是否展示
        /// </summary>
        public bool IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }

        #endregion 
        #region Model
        private int _columnid;
		private int _ocid;
		private int _userid;
		private int _parentid=0;
		private string _title;
		private string _conten;
		private int _orde=1;
		private int _sharerange=1;
		private DateTime _createtime= DateTime.Now;
		private DateTime _updatetime= DateTime.Now;
		private int _contenttype=0;
        private int _haschild = 0;
		/// <summary>
		/// 
		/// </summary>
		public int ColumnID
		{
			set{ _columnid=value;}
			get{return _columnid;}
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
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Conten
		{
			set{ _conten=value;}
			get{return _conten;}
		}
		/// <summary>
		/// 栏目显示顺序
		/// </summary>
		public int Orde
		{
			set{ _orde=value;}
			get{return _orde;}
		}
		/// <summary>
		/// 共享范围： 0 不公开、 1仅教学班学生、 2同课程师生、   4全校师生、  8公开
		/// </summary>
		public int ShareRange
		{
			set{ _sharerange=value;}
			get{return _sharerange;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Updatetime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 0文本模式 ; 1 内页模式;  3列表模式
		/// </summary>
		public int ContentType
		{
			set{ _contenttype=value;}
			get{return _contenttype;}
		}
        /// <summary>
        ///是否有子集
        /// </summary>
        public int HasChild {
            set { _haschild = value; }
            get { return _haschild; }
        }

        public List<OCSiteColumn> Children { get; set; } 

		#endregion Model

	}
}

