using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    public interface IPaper
    {
        int PaperID { get; set; }
        int Type { get; set; }
    }
}
