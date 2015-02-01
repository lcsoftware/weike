using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.CC.OC.Model
{
    [Serializable]
    public class OCMoocVideoInsert
    {
        public int InsertID { get; set; }
        public int ChapterID { get; set; }
        public int FileID { get; set; }
        public int Second { get; set; }
        public string Conten { get; set; }
    }
}
