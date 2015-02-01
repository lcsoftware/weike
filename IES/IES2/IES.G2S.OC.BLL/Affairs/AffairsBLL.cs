using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.DAL;
using IES.G2S.OC.IBLL;
using IES.CC.Affairs.Model;
using IES.JW.Model;

namespace IES.G2S.OC.BLL
{
    public class AffairsBLL : IAffairsBLL
    {
        #region IAffairsBLL 成员

        #region  列表
        public List<OCAffairs> Affairs_List(OCAffairs model, int PageIndex, int PageSize)
        {
            //List<OCAffairs> testlist = new List<OCAffairs>();
            //for (int i = (PageIndex - 1) * PageSize; i < PageIndex*PageSize; i++)
            //{
            //    OCAffairs affairs = new OCAffairs();
            //    affairs.AffairID = i;
            //    affairs.rowscount = 20;
            //    affairs.IsSelected = false;
            //    affairs.UserID = 1000 + i;
            //    affairs.UserName = "1000" + i.ToString();
            //    affairs.OrganizationName = "组织机构" + i.ToString();
            //    affairs.ClassName = "班级";
            //    affairs.AffairType = "补交作业";
            //    affairs.Reson = "忘记了" + i.ToString();
            //    affairs.CreateDate =DateTime.Now;
            //    affairs.AffairDesc = "第一章单元" + i.ToString();

            //    affairs.Status = new Random(0).Next(0,3);
            //    testlist.Add(affairs);
            //}
            //return testlist;
            return AffairsDAL.Affairs_List(model, PageIndex, PageSize);
        }
        #endregion
        public List<Dict> Dict_List(Dict model)
        {
            return AffairsDAL.Dict_List(model);
        }

        #region 详细信息


        #endregion


        #region  新增




        #endregion


        #region 对象更新

        public bool OCAffairs_Status_Upd(OCAffairs model)
        {
            return AffairsDAL.OCAffairs_Status_Upd(model);
        }

        #endregion


        #region 单个批量更新

   
        #endregion


        #region 属性批量操作


        public bool OCAffairs_Beach_Upd(OCAffairs model)
        {
            return AffairsDAL.OCAffairs_Beach_Upd(model);
        }



        #endregion

        #region 删除
        public bool Affairs_Del(OCAffairs model)
        {
            return AffairsDAL.Affairs_Del(model);
        }
        #endregion
        #endregion
    }
}
