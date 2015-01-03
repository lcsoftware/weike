/**  版本信息模板在安装目录下，可自行修改。
* Attachment.cs
*
* 功 能： N/A
* 类 名： Attachment
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/28 16:20:47   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.Resource.Model
{
	/// <summary>
	/// 附件表
	/// </summary>
	[Serializable]
    public partial class Attachment : IResource, IFile
	{
		public Attachment()
		{}
		#region Model
		private int _attachmentid;
		private int _serverid;
		private string _filename;
		private string _title;
		private long _filesize;
		private string _source;
		private int _sourceid=0;
		private DateTime _updatetime= DateTime.Now;
		private string _guid;
        private string _downurl;
        private string _viewurl;
        private string _reffileid;

		/// <summary>
		/// 主键
		/// </summary>
		public int AttachmentID
		{
			set{ _attachmentid=value;}
			get{return _attachmentid;}
		}
		/// <summary>
		/// 存储服务器编号
		/// </summary>
		public int ServerID
		{
			set{ _serverid=value;}
			get{return _serverid;}
		}
		/// <summary>
		/// 文件存储名称（上传后的名称）
		/// </summary>
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// 文件原始名称（上传前的名称）
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 文件大小字节为单位
		/// </summary>
		public long FileSize
		{
			set{ _filesize=value;}
			get{return _filesize;}
		}
		/// <summary>
		/// 文件来源Source，直接用该表的名称：Exercise 习题的附件 ；PaperGroup 试卷分组的附件；OCTeam 教学团队的图片;OC在线课程图片
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 来源表的主键
		/// </summary>
		public int SourceID
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 文件上传时间
		/// </summary>
		public DateTime Updatetime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 临时上传的文件，SourceID未知前 可以用该字段，如果SourceID明确后需要更新sourceID
		/// </summary>
        public string Guid
		{
			set{ _guid=value;}
			get{return _guid;}
		}

        public string DownURL
        {
            set { _downurl = value; }
            get { return _downurl; }
        }

        public string ViewURL
        {
            set { _viewurl = value; }
            get { return _viewurl; }
        }

        public string RefFileID
        {
            set { _reffileid = value; }
            get { return _reffileid; }
        }

		#endregion Model

	}
}

