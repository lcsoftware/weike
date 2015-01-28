using IES.G2S.Resource.DAL;
using IES.G2S.Resource.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.Resource.BLL
{
    public class KeyBLL : IKeyBLL
    {
        public List<IES.Resource.Model.Key> Key_List(IES.Resource.Model.Key model)
        {
            return KeyDAL.Key_List(model);
        }
    }
}
