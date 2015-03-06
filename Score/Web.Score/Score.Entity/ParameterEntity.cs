using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Score.Entity
{
    public class ChangePwdEntity
    {
        public string TeacherID { get; set; }
        public string OldPwd { get; set; }
        public string NewPwd { get; set; }
        public int Status { get; set; }
        public int Result { get; set; }
    }
}
