using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.Model.OC
{
    public  class OCFCResult
    {
        public int ResultID { get; set; }
        public int FCID { get; set; }
        public int GroupID { get; set; }
        public int UserID { get; set; }
        public string  FileName { get; set; }
        public DateTime Submittime { get; set; }
        public int Clicks { get; set; }
        public int FileTypeID { get; set; }
        public string Score { get; set; }

        public int ScoreType { get; set; }

    }
}
