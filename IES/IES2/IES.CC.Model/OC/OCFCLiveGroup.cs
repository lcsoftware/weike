/**  版本信息模板在安装目录下，可自行修改。
* OCFCLiveGroup.cs
*
* 功 能： N/A
* 类 名： OCFCLiveGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:24   N/A    初版

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
	/// 互动分组
	/// </summary>
	[Serializable]
	public partial class OCFCLiveGroup
	{
		public OCFCLiveGroup()
		{}
		#region Model
		private int _id;
		private int _fcgroupid;
		private int _fcliveid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
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
		public int FCLiveID
		{
			set{ _fcliveid=value;}
			get{return _fcliveid;}
		}
		#endregion Model

	}
}

