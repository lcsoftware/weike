/**  版本信息模板在安装目录下，可自行修改。
* Building.cs
*
* 功 能： N/A
* 类 名： Building
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/1 13:35:30   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.JW.Model
{
	/// <summary>
	/// 教学楼
	/// </summary>
	[Serializable]
	public partial class Building
	{
		public Building()
		{}
		#region Model
		private int _buildingid;
		private string _buildingno;
		private string _buildingname;
		private int _zoneid;
		private bool _isdeleted= false;
		/// <summary>
		/// 
		/// </summary>
		public int BuildingID
		{
			set{ _buildingid=value;}
			get{return _buildingid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingNo
		{
			set{ _buildingno=value;}
			get{return _buildingno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildingName
		{
			set{ _buildingname=value;}
			get{return _buildingname;}
		}
		/// <summary>
		/// 校区编号
		/// </summary>
		public int ZoneID
		{
			set{ _zoneid=value;}
			get{return _zoneid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model

	}
}

