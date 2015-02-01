using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class NoticeUser
    {
        public int ID { get; set; }

        public int NoticeID { get; set; }

        public int UserID { get; set; }

        public bool IsDeleted { get; set; }
    }
}
