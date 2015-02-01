using IES.Resource.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.IBLL
{
    public interface IKeyBLL
    {
        List<Key> ExerciseOrFile_Key_List(string SearchKey, string Source, int UserID, int TopNum,int OCID);

        
    }
}
