/**  版本信息模板在安装目录下，可自行修改。
* OCFCGroup.cs
*
* 功 能： N/A
* 类 名： OCFCGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:23   N/A    初版
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
	/// 翻转课堂学生分组
	/// </summary>
	[Serializable]
	public partial class OCFCGroup
	{
		public OCFCGroup()
		{}
		#region Model
		private int _fcgroupid;
		private int _ocfcid;
		private int _usernum;
		private int _groupname;
		private int _groupscope=1;
		private int _choice=1;
		private int _studentleader=1;
		private int _teacher=0;
		private bool _isdefault= false;
		/// <summary>
		/// 
		/// </summary>
		public int FCGroupID
		{
			set{ _fcgroupid=value;}
			get{return _fcgroupid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OCFCID
		{
			set{ _ocfcid=value;}
			get{return _ocfcid;}
		}
		/// <summary>
		/// 小组人数
		/// </summary>
		public int UserNum
		{
			set{ _usernum=value;}
			get{return _usernum;}
		}
		/// <summary>
		/// 小组名称1 数字； 2字母 ； 3颜色；4动物；5水果；6蔬菜
		/// </summary>
		public int GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 1教学班内分组  ；2 跨教学班分组
		/// </summary>
		public int GroupScope
		{
			set{ _groupscope=value;}
			get{return _groupscope;}
		}
		/// <summary>
		/// 1 系统随机； 2男女分开；3男女按比例
		/// </summary>
		public int Choice
		{
			set{ _choice=value;}
			get{return _choice;}
		}
		/// <summary>
		/// 0 无组长；1：随机组长；3：指定组长
		/// </summary>
		public int StudentLeader
		{
			set{ _studentleader=value;}
			get{return _studentleader;}
		}
		/// <summary>
		/// 0：无指导老师； >0每组随机分配*人
		/// </summary>
		public int Teacher
		{
			set{ _teacher=value;}
			get{return _teacher;}
		}
		/// <summary>
		/// 设置为默认分组模式
		/// </summary>
		public bool IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		#endregion Model

	}
}

