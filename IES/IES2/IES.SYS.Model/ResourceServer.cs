/**  版本信息模板在安装目录下，可自行修改。
* ResourceServer.cs
*
* 功 能： N/A
* 类 名： ResourceServer
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/2 9:12:46   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace IES.SYS.Model
{
	/// <summary>
	/// 系统通知
	/// </summary>
	[Serializable]
	public partial class ResourceServer
	{
		public ResourceServer()
		{}
		#region Model
		private int _serverid;
		private string _host;
		private string _iisfolder;
		private string _iisport;
		private string _mmsfolder;
		private string _mmsport;
		private string _nginxfolder;
		private string _nginxport;
		private string _pubkey;
		private bool _ismainserver= true;
		/// <summary>
		/// 
		/// </summary>
		public int ServerID
		{
			set{ _serverid=value;}
			get{return _serverid;}
		}
		/// <summary>
		/// 服务器IP
		/// </summary>
		public string Host
		{
			set{ _host=value;}
			get{return _host;}
		}
		/// <summary>
		/// IIS目录文件夹
		/// </summary>
		public string IISFolder
		{
			set{ _iisfolder=value;}
			get{return _iisfolder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IISPort
		{
			set{ _iisport=value;}
			get{return _iisport;}
		}
		/// <summary>
		/// 媒体发布点
		/// </summary>
		public string MMSFolder
		{
			set{ _mmsfolder=value;}
			get{return _mmsfolder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MMSPort
		{
			set{ _mmsport=value;}
			get{return _mmsport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NginxFolder
		{
			set{ _nginxfolder=value;}
			get{return _nginxfolder;}
		}
		/// <summary>
		/// 视频点播端口
		/// </summary>
		public string NginxPort
		{
			set{ _nginxport=value;}
			get{return _nginxport;}
		}
		/// <summary>
		/// 秘钥
		/// </summary>
		public string PubKey
		{
			set{ _pubkey=value;}
			get{return _pubkey;}
		}
		/// <summary>
		/// 是否是分发服务器
		/// </summary>
		public bool IsMainServer
		{
			set{ _ismainserver=value;}
			get{return _ismainserver;}
		}
		#endregion Model

	}
}

