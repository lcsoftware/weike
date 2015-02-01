using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.Resource.Model
{
     [Serializable]
     public class ChapterTest
     {
         #region Model

         public int TestID { get; set; }
         public int OCID { get; set; }

         public int ChapterID { get; set; }
         /// <summary>
         /// 是否必读
         /// </summary>
         public int IsMust { get; set; }
         public int Orde { get; set; }
         public int IsFinish { get; set; }
         #endregion
     }
}
