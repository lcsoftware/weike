using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IES.JW.Model
{
    public class SpecialtyCommon : ISpecialty
    {
        public int SpecialtyID { get; set; }

        public Specialty specialty { get; set; }

        public List<Specialty> specialtylist { get; set; }

        public List<SpecialtySite> specialtysitelist { get; set; }

        public List<SpecialtyTeacher> specialtyteacherlist { get; set; }
    }
}
