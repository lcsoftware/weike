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
    public class PaperBLL:IPaperBLL
    {

        #region 列表
        public List<Paper> Paper_Search(string Searchkey, int OCID, int PaperType, int Scope, DateTime UploadTime, int ShareRange, int UserID, int PageSize, int PageIndex)
        {
            Paper paper = new Paper();
            paper.Papername = Searchkey;
            paper.OCID = OCID;
            paper.Type = PaperType;
            paper.Scope = Scope;
            paper.UpdateTime = UploadTime;
            paper.ShareScope = ShareRange;
            paper.CreateUserID = UserID;
            return PaperDAL.Paper_Search(paper,10000,1);
        }

        public List<Paper> Paper_Search(Paper paper, int PageSize, int PageIndex)
        {
            return PaperDAL.Paper_Search(paper, PageSize, PageIndex);
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

        public int Paper_ADD(Paper model)
        {
            return PaperDAL.Paper_ADD(model);
        }

        public int PaperGroup_ADD(PaperGroup model)
        {
            return PaperDAL.PaperGroup_ADD(model);
        }

        public bool PaperExercise_ADD(int PaperID, int PaperGroupID, int ExerciseID, int Score, int Order)
        {
            return PaperDAL.PaperExercise_ADD(PaperID, PaperGroupID, ExerciseID, Score, Order);
        }

        public bool PaperUpd(Paper model)
        {
            return PaperDAL.PaperUpd(model);
        }

        public bool PaperGroupUpd(PaperGroup model)
        {
            return PaperDAL.PaperGroupUpd(model);
        }

        public bool PaperTactic_Edit(PaperTactic model)
        {
            return PaperDAL.PaperTactic_Edit(model);
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
