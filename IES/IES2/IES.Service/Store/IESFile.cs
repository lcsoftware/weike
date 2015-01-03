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

        public string DownURL { get; set; }

        public string ViewURL { get; set; }

        public string FileGuid { get; set; }

    }
}
