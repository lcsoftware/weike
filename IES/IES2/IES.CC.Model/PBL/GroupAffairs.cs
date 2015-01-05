/**  版本信息模板在安装目录下，可自行修改。
* GroupAffairs.cs
*
* 功 能： N/A
* 类 名： GroupAffairs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/14 17:44:56   N/A    初版
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
	/// 小组事务
	/// </summary>
	[Serializable]
	public partial class GroupAffairs
	{
		public GroupAffairs()
		{}


        #region 补充信息

        /// <summary>
        /// 小组成员姓名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 小组的名称
        /// </summary>
        public string name { get; set; }


        #endregion 

		#region Model
		private int _affairsid;
		private int _userid;
		private int _togroupid;
		private DateTime _createtime= DateTime.Now;
		private int _status=1;
		private string _reason;
		/// <summary>
		/// 
		/// </summary>
		public int AffairsID
		{
			set{ _affairsid=value;}
			get{return _affairsid;}
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
		public int ToGroupID
		{
			set{ _togroupid=value;}
			get{return _togroupid;}
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
		/// 1 申请中；2同意；3拒绝
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 拒绝理由
		/// </summary>
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		#endregion Model

	}
}

