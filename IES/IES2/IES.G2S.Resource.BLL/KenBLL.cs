using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.Resource.DAL;
using IES.Resource.Model;
using IES.AOP.G2S;
using IES.G2S.Resource.IBLL;


namespace IES.G2S.Resource.BLL
{
    /// <summary>
    /// 知识点
    /// </summary>
    public class KenBLL : IKenBLL
    { 
        public IList<Ken> Ken_List(Ken model)
        {
            return KenDAL.Ken_List(model);
        }
        public bool Ken_Del( Ken model)
        {
            return KenDAL.Ken_Del(model);
        }

        public Ken Ken_ADD(Ken model)
        {
            return  KenDAL.Ken_ADD(model);
        }

        public bool Ken_Upd(Ken model)
        {
           return   KenDAL.Ken_Upd(model);
        }
    }
}
