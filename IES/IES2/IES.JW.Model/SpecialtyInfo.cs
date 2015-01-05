using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.JW.Model
{
    public class SpecialtyInfo:ISpecialty
    {
        public int SpecialtyID { get; set; }

        public SpecialtyCommon specialtycommon { get; set; }
    }
}
