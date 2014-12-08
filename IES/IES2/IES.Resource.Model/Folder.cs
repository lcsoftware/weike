/**  版本信息模板在安装目录下，可自行修改。
* Folder.cs
*
* 功 能： N/A
* 类 名： Folder
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/12/4 17:26:49   N/A    初版
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
    /// 文件夹
    /// </summary>
    [Serializable]
    public partial class Folder:IResource
    {
        public Folder()
        { }
        #region Model
        private int _folderid;
        private int _createuserid = 0;
        private int _owneruserid = 0;
        private int _ocid = 0;
        private int _courseid = 0;
        private int _parentid = 0;
        private string _foldername;
        private int _sharerange = 1;
        private string _brief;
        private int _orde = 1;
        private DateTime _createtime = DateTime.Now;
        private bool _isdeleted = false;
        /// <summary>
        /// 主键
        /// </summary>
        public int FolderID
        {
            set { _folderid = value; }
            get { return _folderid; }
        }
        /// <summary>
        /// 文件夹创建人编号
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
        /// 在线课程编号 0 表示我的资料库，>0 表示在线课程编号
        /// </summary>
        public int OCID
        {
            set { _ocid = value; }
            get { return _ocid; }
        }
        /// <summary>
        /// 0 表示我的资料库，>0 表示课程编号
        /// </summary>
        public int CourseID
        {
            set { _courseid = value; }
            get { return _courseid; }
        }
        /// <summary>
        /// 0 表示第一级目录，上级文件夹编号
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName
        {
            set { _foldername = value; }
            get { return _foldername; }
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
        /// 描述
        /// </summary>
        public string Brief
        {
            set { _brief = value; }
            get { return _brief; }
        }
        /// <summary>
        /// 文件夹排序
        /// </summary>
        public int Orde
        {
            set { _orde = value; }
            get { return _orde; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
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

