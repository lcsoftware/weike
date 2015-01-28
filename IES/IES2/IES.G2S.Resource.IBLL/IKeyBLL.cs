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
        List<Key> Key_List(Key model);
    }
}
