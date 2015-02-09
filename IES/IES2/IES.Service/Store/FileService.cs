using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using IES.G2S.JW.BLL;
using IES.JW.Model;
using IES.Cache;
using IES.Resource.Model;
using IES.G2S.Resource.BLL;
using IES.Common;



namespace IES.Service
{
    public class   FileService
    {
        public const string downurl = "http://{0}:{1}/{2}/{3}";

        /// <summary>
        /// 在线浏览或视频点播的地址
        /// </summary>
        public const string viewurl = "{0}://{1}:{2}/{3}/{4}";

        #region 判断文件存在否

        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static bool RemoteFileExists(string fileUrl)
        {
            try
            {
                HttpWebRequest re = (HttpWebRequest)WebRequest.Create(fileUrl);
                HttpWebResponse res = (HttpWebResponse)re.GetResponse();
                if (res.ContentLength != 0)
                {
                    return true;
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// 判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static bool RemoteFileExists(IFile  file)
        {
            try
            {
                string fileurl = FileDownURL(file);
                HttpWebRequest re = (HttpWebRequest)WebRequest.Create(fileurl);
                HttpWebResponse res = (HttpWebResponse)re.GetResponse();
                if (res.ContentLength != 0)
                {
                    return true;
                }
            }
            catch (System.Exception e)
            {
                return false;
            }
            return false;
        }

        #endregion 

        #region 获取系统所有的附件表
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<Attachment> Attachment_List(Attachment model)
        {
            AttachmentBLL attachmentBLL = new AttachmentBLL();
            return attachmentBLL.Attachment_List(model);
        }

        public static List<Attachment> Attachment_UserIMG_List()
        {
            Attachment model = new Attachment { Source = "User" };
            AttachmentBLL attachmentBLL = new AttachmentBLL();
            return attachmentBLL.Attachment_List(model);
        }




        #endregion 

        #region  IES文件上传
        /// <summary>
        /// 附件添加
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<Attachment> AttachmentUpload()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            List<Attachment> attachmentlist = new List<Attachment>();

            for ( int i = 0; i < files.Count; i++ )
            {
                IESFile file = Upload(files[i]);
                if (file.FileGuid != null && file.FileGuid != string.Empty)
                {
                    if (RemoteFileExists(file))
                    {
                        AttachmentBLL bll = new AttachmentBLL();
                        Attachment attachment = new Attachment
                        {
                            FileName = file.FileName,
                            ServerID = file.ServerID,
                            FileSize = file.FileSize,
                            Title = file.FileTitle,
                            Guid = file.FileGuid,
                            Source = string.Empty,
                            SourceID = 0 ,
                            RefFileID = "0"                   
                        };
                        if(bll.Attachment_ADD(attachment))
                            attachmentlist.Add(attachment);  
                    }
                }
            }
            return attachmentlist;
        }


        /// <summary>
        /// 附件关联
        /// </summary>
        /// <param name="attachmentlist"></param>
        /// <returns></returns>
        public static bool AttachmentRelation(  Attachment attachment  ) 
        {
            return IES.G2S.Resource.DAL.AttachmentDAL.Attachment_SourceID_Upd(attachment);
        }




        /// <summary>
        /// 资料上传
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<IES.Resource.Model.File> ResourceFileUpload( IES.Resource.Model.File model)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            List<IES.Resource.Model.File> resourcefilelist = new List<IES.Resource.Model.File>();

            for (int i = 0; i < files.Count; i++)
            {
                IESFile file = Upload(files[i]);
                if (file.FileGuid != null && file.FileGuid != string.Empty)
                {
                    if (RemoteFileExists(file))
                    {
                        FileBLL bll = new FileBLL();
                        IES.Resource.Model.File resourcefile = new IES.Resource.Model.File
                        {
                            FileName = file.FileName,
                            ServerID = file.ServerID,
                            FileSize = file.FileSize,
                            FileTitle = file.FileTitle,
                            OCID = model.OCID ,
                            FolderID = model.FolderID,
                            CourseID = model.CourseID ,
                            CreateUserID = UserService.CurrentUser.UserID ,
                            CreateUserName = UserService.CurrentUser.UserName  ,
                            Ext = file.Ext ,
                            ShareRange = model.ShareRange 
                        };
                        resourcefile = bll.File_ADD(resourcefile);
                        if (resourcefile.FileID > 0 )
                            resourcefilelist.Add(resourcefile);
                    }
                }
            }
            return resourcefilelist ;
        }


        #endregion 

        #region 文件下载或在线浏览地址

        public static string FileDownURL(IFile file)
        {
            if (file is Attachment)
                file = file as Attachment;
            else if (file is IESFile)
                file = file as IESFile;
            else if (file is IES.Resource.Model.File)
                file = file as IES.Resource.Model.File;

            ResourceServer server = StoreServie.ResourceServer_Get(file.ServerID);
            return string.Format(downurl, server.Host, server.IISPort, server.IISFolder, file.FileName);
        }


        /// <summary>
        /// 文件在线浏览地址 ：TODO
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string FileViewURL(IFile file)
        {
            if (file is Attachment)
                file = file as Attachment;
            else if (file is IESFile)
                file = file as IESFile;
            else if (file is IES.Resource.Model.File)
                file = file as IES.Resource.Model.File;

            string ext = StringHelp.GetFileNameExt(file.FileName).ToLower();


            ResourceServer server = StoreServie.ResourceServer_Get(file.ServerID);


            return string.Format(downurl, server.Host, server.IISPort, server.IISFolder, file.FileName);
        }

        #endregion 

        #region  文件上传核心方法

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="postFile"></param>
        /// <returns></returns>
        private static IESFile Upload(HttpPostedFile postFile)
        {

            return _upload(postFile.InputStream, postFile.FileName);
        }

        private static IESFile _upload(Stream inputStream, string fileName)
        {

            ResourceServer server = StoreServie.ResourceServer_Fast_Get();

            string machineName = server.Host;
            string httpPort = server.IISPort;
            string webFolder = server.IISFolder;
            string PubKey = server.PubKey;

            string filehead = Guid.NewGuid().ToString();
            string newFileName = filehead + Path.GetExtension(fileName);

            string uri = string.Format("http://{0}:{1}/{2}/HttpUpload/HttpUpload.ashx?PubKey={3}&fileName={4}&UName=able&PWD=able",
                machineName, httpPort, webFolder, PubKey, newFileName);

            HttpWebRequest webRequest = WebRequest.Create(uri) as HttpWebRequest;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = inputStream.Length;
            webRequest.Timeout = 99999999;
            webRequest.Method = "POST";
            webRequest.AllowWriteStreamBuffering = false;

            bool iscorrect = true;

            using (Stream fStream = inputStream)
            {
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    try
                    {
                        byte[] buff = new byte[40960];
                        int len = 0;
                        while ((len = fStream.Read(buff, 0, buff.Length)) > 0)
                        {
                            requestStream.Write(buff, 0, len);
                            fStream.Flush();
                            requestStream.Flush();
                        }
                        WebResponse webResponse = webRequest.GetResponse();
                    }
                    catch(Exception ex)
                    {
                        iscorrect = false;
                    }
                }
            }
            IESFile file = new IESFile();
            if (iscorrect)
            {
                file.ServerID = server.ServerID;
                file.FileName = newFileName;
                file.FileGuid = filehead;
                file.UserID = UserService.CurrentUser.UserID;
                file.FileSize = webRequest.ContentLength;
                file.FileTitle = fileName;
            }
            return file;
        }


        #endregion 

    }
}
