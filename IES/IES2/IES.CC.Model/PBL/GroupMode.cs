/**  版本信息模板在安装目录下，可自行修改。
* GroupMode.cs
*
* 功 能： N/A
* 类 名： GroupMode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:57   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.CC.Model.PBL
{
	/// <summary>
	/// 分组模式表
	/// </summary>
	[Serializable]
	public partial class GroupMode
	{
		public GroupMode()
		{}
		#region Model
		private int _groupmodeid;
		private string _name;
		private int _ocid;
		private int _groups=0;
		private bool _israndgender= true;
		private bool _isinclass= true;
		private int _userid;
		private DateTime _createtime= DateTime.Now;
		private bool _isallowchangegroup= false;
		/// <summary>
		/// 
		/// </summary>
		public int GroupModeID
		{
			set{ _groupmodeid=value;}
			get{return _groupmodeid;}
		}
		/// <summary>
		/// 分组方式名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		/// 需要分的小组数量；如果是班内分组，则指每个教学班的分组数量
		/// </summary>
		public int Groups
		{
			set{ _groups=value;}
			get{return _groups;}
		}
		/// <summary>
		/// 1 男女随机；0 按比例分配
		/// </summary>
		public bool IsRandGender
		{
			set{ _israndgender=value;}
			get{return _israndgender;}
		}
		/// <summary>
		/// 1班内分组； 2跨班分组
		/// </summary>
		public bool IsInClass
		{
			set{ _isinclass=value;}
			get{return _isinclass;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
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
		/// 是否允许学生申请换组
		/// </summary>
		public bool IsAllowChangeGroup
		{
			set{ _isallowchangegroup=value;}
			get{return _isallowchangegroup;}
		}

        /// <summary>
        /// 是否每次任务随机分组
        /// </summary>
        public bool  IsRand { get; set; }




		#endregion Model

	}
}

