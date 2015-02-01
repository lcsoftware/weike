using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
    [Serializable]
    public class ChapterInfo
    {
        public List<Chapter> Chapters { get; set; }

        public List<ChapterTest> ChapterTests { get; set; } 
    }
}
