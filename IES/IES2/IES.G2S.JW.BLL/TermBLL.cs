using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES.G2S.JW.BLL
{
    public class TermBLL:ITermBLL
    {
        #region 列表

        public List<Term> Term_List(Term model)
        {
            return TermDAL.Term_List(model);
        }

        public List<TermInfo> TermInfo_List()
        {
            Term term = new Term();
            TermInfo termInfo = new TermInfo();
            term.TermYear = "";
            term.TermTypeID = 0;
            
            List<TermInfo> listTermInfo = new List<TermInfo>();
            List<Term> listTerm = Term_List(term);
            List<TermType> listTermType = TermDAL.TermType_List();

            

            foreach (var item in listTerm)
            {
                termInfo.term = item;
                termInfo.termTypeName = listTermType.Find(x => x.TermTypeID == item.TermTypeID).TermTypeName;
                listTermInfo.Add(termInfo);
            }

            return listTermInfo;

        }

        #endregion

        #region 新增或修改

        public Term Term_Edit(Term model)
        {
            return TermDAL.Term_Edit(model);
        }

	    #endregion

        #region 删除

        public bool Term_Del(Term model)
        {
            return TermDAL.Term_Del(model);
        }

        #endregion

        #region  根据学期获取课节
        public List<Lesson> Lesson_List(Term model)
        {
            return TermDAL.Lesson_List(model);
        }
        #endregion

        #region  校历详细信息
        public TermInfo TermInfo_Get(Term model)
        {
            return TermDAL.TermInfo_Get(model);
        }
        #endregion

        #region  Lesson新增或修改
        /// <summary>
        /// 课节添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Lesson Lesson_Edit(Lesson model)
        {
            return TermDAL.Lesson_Edit(model);
        }
        #endregion

        #region  Lesson删除
        /// <summary>
        /// 课节删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Lesson_Del(Lesson model)
        {
            return TermDAL.Lesson_Del(model);
        }
        #endregion
    }
}
