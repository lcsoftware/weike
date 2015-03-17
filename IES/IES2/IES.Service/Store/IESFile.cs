using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.Service
{
    public class IESFile:IResource ,IFile 
    {
        public int ID { get; set; }

        public string FileName { get; set; }

        public string FileTitle { get; set; }

        public int  ServerID { get; set; }

        public long FileSize { get; set; }

        public int UserID { get; set; }

        public string Ext { get; set; }

        public int FileType { get; set; }

        public string DownURL { get; set; }

        public string ViewURL { get; set; }

        public string FileGuid { get; set; }

        public int  GetFileType()
        {
            if (Ext.ToLower().Contains("mp4") || Ext.ToLower().Contains("wmv") || Ext.ToLower().Contains("asf") || Ext.ToLower().Contains("flv") || Ext.ToLower().Contains("swf"))
                return 1;
            else if (Ext.ToLower().Contains("doc"))
                return 2;
            else if (Ext.ToLower().Contains("xls"))
                return 3;
            else if (Ext.ToLower().Contains("ppt"))
                return 4;
            else if (Ext.ToLower().Contains("pdf"))
                return 5;
            else if (Ext.ToLower().Contains("jpg") || Ext.ToLower().Contains("jpeg") || Ext.ToLower().Contains("gif") || Ext.ToLower().Contains("png") || Ext.ToLower().Contains("bmp"))
                return 6;
            else if (Ext.ToLower().Contains("mp3") || Ext.ToLower().Contains("wma"))
                return 7;
            else if (Ext.ToLower().Contains("zip") || Ext.ToLower().Contains("rar"))
                return 8;
            else
                return 9;
        }
        


    }
}
