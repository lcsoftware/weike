using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;

namespace IES.G2S.Resource.IBLL
{
    public interface IChapterBLL
    {
        bool Chapter_Del(Chapter model);

        bool Chapter_ADD(Chapter model);

        bool Chapter_Upd(Chapter model);

    }
}
