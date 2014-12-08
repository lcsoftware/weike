/**  版本信息模板在安装目录下，可自行修改。
* File.cs
*
* 功 能： N/A
* 类 名： File
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:47   N/A    初版
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
    /// 文件表
    /// </summary>
    [Serializable]
    public partial class File:IResource 
    {
        public File()
        { }
        #region Model
        private int _fileid;
        private int _folderid;
        private int _ocid = 0;
        private int _courseid = 0;
        private int _subjectid1 = 0;
        private int _subjectid2 = 0;
        private int _createuserid;
        private int _owneruserid;
        private string _filetitle;
        private string _filename;
        private string _ext;
        private int _filetype = 9;
        private string _brief;
        private string _keys;
        private int _filesize = 0;
        private string _pingyin;
        private int _timelength = 0;
        private string _rarindexpage;
        private DateTime _uploadtime = DateTime.Now;
        private int _orde = 1;
        private int _sharerange = 1;
        private bool _allowdownload = false;
        private int _serverid;
        private int _clicks = 0;
        private int _downloads = 0;
        private int _isdistribute = 0;
        private int _istransfer = 0;
        private bool _isdeleted = false;
        /// <summary>
        /// 
        /// </summary>
        public int FileID
        {
            set { _fileid = value; }
            get { return _fileid; }
        }

        /// <summary>
        /// 文件夹编号
        /// </summary>
        public int FolderID
        {
            set { _folderid = value; }
            get { return _folderid; }
        }
        /// <summary>
        /// 我的资料库为0 ，>0为在线课程编号
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 0 表示我的资料库，>1 表示课程的编号
        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 学科分类1
        /// </summary>
        public int SubjectID1
        {
            set { _subjectid1 = value; }
            get { return _subjectid1; }
        }
        /// <summary>
        /// 学科分类2
        /// </summary>
        public int SubjectID2
        {
            set { _subjectid2 = value; }
            get { return _subjectid2; }
        }
        /// <summary>
        /// 创建人编号
        /// </summary>
        public int CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OwnerUserID
        {
            set { _owneruserid = value; }
            get { return _owneruserid; }
        }
        /// <summary>
        /// 文件原始名称
        /// </summary>
        public string FileTitle
        {
            set { _filetitle = value; }
            get { return _filetitle; }
        }
        /// <summary>
        /// 文件上传的名称
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 文件的扩展名
        /// </summary>
        public string Ext
        {
            set { _ext = value; }
            get { return _ext; }
        }
        /// <summary>
        /// 文件类型：   1 视频 ； 2 音频； 3 word；4 ppt ； 5Excel ；6PDF；7 图片；8压缩包；9其他
        /// </summary>
        public int FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 文件描述信息
        /// </summary>
        public string Brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
        /// <summary>
        /// 关键字 多个关键字之间用空格
        /// </summary>
        public string Keys
        {
            set { _keys = value; }
            get { return _keys; }
        }
        /// <summary>
        /// 文件大小 字节单位
        /// </summary>
        public int FileSize
        {
            set { _filesize = value; }
            get { return _filesize; }
        }
        /// <summary>
        /// 文件拼音
        /// </summary>
        public string pingyin
        {
            set { _pingyin = value; }
            get { return _pingyin; }
        }
        /// <summary>
        /// 视频文件时长
        /// </summary>
        public int TimeLength
        {
            set { _timelength = value; }
            get { return _timelength; }
        }
        /// <summary>
        /// 对于压缩包HTML文件，访问首页名称
        /// </summary>
        public string RarIndexPage
        {
            set { _rarindexpage = value; }
            get { return _rarindexpage; }
        }
        /// <summary>
        /// 文件上传时间
        /// </summary>
        public DateTime UploadTime
        {
            set { _uploadtime = value; }
            get { return _uploadtime; }
        }
        /// <summary>
        /// 文件显示顺序
        /// </summary>
        public int Orde
        {
            set { _orde = value; }
            get { return _orde; }
        }
        /// <summary>
        /// 共享范围： 0 不公开、 1仅教学班学生、2同课程教学团队师生、4同课程师生、   8全校师生、  16公开
        /// </summary>
        public int ShareRange
        {
            set { _sharerange = value; }
            get { return _sharerange; }
        }
        /// <summary>
        /// 是否允许下载
        /// </summary>
        public bool AllowDownload
        {
            set { _allowdownload = value; }
            get { return _allowdownload; }
        }
        /// <summary>
        /// 存储服务器的编号
        /// </summary>
        public int ServerID
        {
            set { _serverid = value; }
            get { return _serverid; }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int Clicks
        {
            set { _clicks = value; }
            get { return _clicks; }
        }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int Downloads
        {
            set { _downloads = value; }
            get { return _downloads; }
        }
        /// <summary>
        /// 文件分发状态：0仅主存储； 镜像存储分发状态 1仅第一台存储  ；2仅第二台存储；4仅第三台；8仅第四台；16仅第五台；32仅第六台；64仅第七台；128仅第八台
        /// </summary>
        public int IsDistribute
        {
            set { _isdistribute = value; }
            get { return _isdistribute; }
        }
        /// <summary>
        /// doc、ppt、pdf、视频 文件是否转换成功   0:未转换 1:成功 2:失败
        /// </summary>
        public int IsTransfer
        {
            set { _istransfer = value; }
            get { return _istransfer; }
        }
        /// <summary>
        /// 删除状态
        /// </summary>
        public bool IsDeleted
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model

    }
}

