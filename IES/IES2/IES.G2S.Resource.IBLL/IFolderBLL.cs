using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.Resource.Model;


namespace IES.G2S.Resource.IBLL
{
    public interface IFolderBLL
    {
        void Folder_Del(int id);

        Folder Folder_ADD(Folder model);
    }
}
