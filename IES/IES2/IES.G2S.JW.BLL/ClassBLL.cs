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
        public List<Class> Class_List(Class model, int PageIndex, int PageSize)
        {
            return ClassDAL.Class_List(model, PageIndex, PageSize);
        }
        #endregion

        #region  详细信息

        public Class Class_Get(int ClassID)
        {
            return ClassDAL.Class_Get(ClassID);
        }

        public List<User> ClassStudent_List(int ClassID)
        {
            return ClassDAL.ClassStudent_List(ClassID);
        }

        #endregion

        #region  对象修改或新增

        public Class Class_Edit(Class model)
        {

            return ClassDAL.Class_Edit(model);
        }

        //修改行政班下学生
        public bool ClassStudent_Save(Class model)
        {
            return ClassDAL.ClassStudent_Save(model);
        }
        #endregion

        #region 单个对象更新



        #endregion

        #region 批量删除
        public bool Class_Batch_Del(string IDS)
        {
            return ClassDAL.Class_Batch_Del(IDS);
        }

        #endregion

        #region 删除

        public bool Class_Del(Class model)
        {
            return ClassDAL.Class_Del(model);
        }

        #endregion

    }
}
