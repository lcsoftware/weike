using IES.G2S.JW.DAL;
using IES.AOP.G2S;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IES.G2S.JW.BLL
{
    public class ClassBLL : IClassBLL
    {
        #region  列表
        public List<Class> Class_List(Class model, int UserID, int PageIndex, int PageSize)
        {
            return ClassDAL.Class_List(model, UserID, PageIndex, PageSize);
        }
        #endregion

        #region  详细信息

        public IES.JW.Model.IClass Class_Get(IClass model)
        {
            Class o = model as Class;

            return ClassDAL.Class_Get(model);
        }

        #endregion

        #region  新增

        public Class Class_Add(Class model)
        {
            return ClassDAL.Class_ADD(model);
        }

        #endregion

        #region  对象更新

        public bool Class_Upd(Class model , out string opt )
        {

            return ClassDAL.Class_Upd(model, out opt );
        }

        #endregion

        #region 单个对象更新



        #endregion

        #region 批量属性操作



        #endregion

        #region 删除

        public bool Class_Del(Class model)
        {
            return ClassDAL.Class_Del(model);
        }

        #endregion
    }
}
