using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.JW.DAL;
using IES.JW.Model;
using IES.G2S.JW.IBLL;

namespace IES.G2S.JW.BLL
{
    public class OCDefaultColumnBLL
    {

        #region 列表
        public List<OCDefaultColumn> OCDefaultColumn_List()
        {
            return OCDefaultColumnDAL.OCDefaultColumn_List();
        }
        #endregion

        #region 新增
        public OCDefaultColumn OCDefaultColumn_ADD(OCDefaultColumn model)
        {
            return OCDefaultColumnDAL.OCDefaultColumn_ADD(model);
        }
        #endregion

        #region 更新
        public bool OCDefaultColumn_Edit(OCDefaultColumn model)
        {
            return OCDefaultColumnDAL.OCDefaultColumn_Edit(model);
        }
        #endregion

        #region 删除
        public bool OCDefaultColumn_Del(OCDefaultColumn model)
        {
            return OCDefaultColumnDAL.OCDefaultColumn_Del(model);
        }
        #endregion

        #region 重命名
        /// <summary>
        /// 
        /// </summary>
        public bool OCDefaultColumn_ReName(OCDefaultColumn model)
        {
            return OCDefaultColumnDAL.OCDefaultColumn_ReName(model);
        }
        #endregion


    }
}
