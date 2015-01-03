using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.SYS.Model
{
    public  class Menu
    {
        public string MenuID { get; set; }

        public string Title { get; set; }

        public string TitleEn { get; set; }

        public string ParentID { get; set; }

        public string ModuleID { get; set; }

        public int Scope { get; set; }

        public int Orde { get; set; }

        public string URL { get; set; }




    }
}
