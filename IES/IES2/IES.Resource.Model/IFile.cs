using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
   public  interface IFile
    {
        int ServerID { get; set; }

        string FileName { get; set; }

    }
}
