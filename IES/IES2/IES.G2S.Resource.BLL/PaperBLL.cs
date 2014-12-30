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
    public class PaperBLL
    {

        #region 列表
        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public List<Paper> Paper_Search(Paper model, int PageSize, int PageIndex)
        {
            return PaperDAL.Paper_Search(model, PageSize, PageIndex);
        }

        #endregion


        #region 获取单个实体

        /// <summary>
        /// 创建试卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IES.Resource.Model.IPaper   Paper_Get( Paper    model )
        {
            return PaperDAL.CreatePaper( model );
        }


        #endregion

        #region 新增
        public bool Paper_ADD(IResource model)
        {

            if (model is Paper)
            {
                PaperDAL.Paper_ADD(model as Paper);
            }

            if (model is  PaperCardInfo )
            {
                PaperDAL.PaperCardInfo_ADD(model as PaperCardInfo );
            }

            if (model is  PaperDefineInfo )
            {
                PaperDAL.PaperDefineInfo_ADD(model as PaperDefineInfo);
            }

            return true;
        }

        #endregion

        #region  删除
     
        public bool Exercise_Del(Paper  mode)
        {
            return PaperDAL.Paper_Del(mode);
        }

        #endregion 
    }
}
