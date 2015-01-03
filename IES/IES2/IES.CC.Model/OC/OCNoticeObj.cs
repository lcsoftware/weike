/**  版本信息模板在安装目录下，可自行修改。
* OCNoticeObj.cs
*
* 功 能： N/A
* 类 名： OCNoticeObj
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 20:19:28   N/A    初版
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
	/// 课程通知发放对象，以教学班为一个单位
	/// </summary>
	[Serializable]
	public partial class OCNoticeObj
	{


        #region 补充信息

        /// <summary>
        /// j教学班名称
        /// </summary>
        public string classname { get; set; }



        #endregion 


		public OCNoticeObj()
		{}
		#region Model
		private int _id;
		private int _noticeid;
		private int _teachingclassid;
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
		public int NoticeID
		{
			set{ _noticeid=value;}
			get{return _noticeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TeachingClassID
		{
			set{ _teachingclassid=value;}
			get{return _teachingclassid;}
		}
		#endregion Model

	}
}

