/**  版本信息模板在安装目录下，可自行修改。
* OCFCOffline.cs
*
* 功 能： N/A
* 类 名： OCFCOffline
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:25   N/A    初版
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
	/// 线下课堂设置
	/// </summary>
	[Serializable]
	public partial class OCFCOffline
	{
		public OCFCOffline()
		{}
		#region Model
		private int _fcofflineid;
		private int _ocfcid;
		private string _title;
		private DateTime _starttime;
		private DateTime _endtime;
		private int _userid;
		private int? _assuserid;
		private string _address;
		private string _brief;
		private int? _evascore;
		/// <summary>
		/// 
		/// </summary>
		public int FCOfflineID
		{
			set{ _fcofflineid=value;}
			get{return _fcofflineid;}
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
		public DateTime StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
		/// 教师编号
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 助教编号
		/// </summary>
		public int? AssUserID
		{
			set{ _assuserid=value;}
			get{return _assuserid;}
		}
		/// <summary>
		/// 授课地点
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 授课要求
		/// </summary>
		public string Brief
		{
			set{ _brief=value;}
			get{return _brief;}
		}
		/// <summary>
		/// 课堂签到分值
		/// </summary>
		public int? EvaScore
		{
			set{ _evascore=value;}
			get{return _evascore;}
		}
		#endregion Model

	}
}

