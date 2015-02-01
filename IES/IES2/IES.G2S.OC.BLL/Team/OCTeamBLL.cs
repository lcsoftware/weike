using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.G2S.OC.IBLL.Team;
using IES.CC.OC.Model;
using IES.G2S.OC.DAL.Team;
using IES.Cache;
using IES.Common;

namespace IES.G2S.OC.BLL.Team
{
    public class OCTeamBLL : IOCTeamBLL
    {
        #region  列表
        /// <summary>
        /// 获取所有的在线课程教学团队列表
        /// </summary>
        /// <returns></returns>
        public List<OCTeam> OCTeam_Cache_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "OCTeam_Cache_List"))
            {
                List<OCTeam> octeamlist = OCTeamDAL.OCTeam_Cache_List();
                cache.Set(string.Empty, "OCTeam_Cache_List", octeamlist);
                return octeamlist;
            }
            else
            {
                return cache.Get<List<OCTeam>>(string.Empty, "OCTeam_Cache_List");
            }
        }
        /// <summary>
        /// 获取所有的在线课程教学团队教学班列表
        /// </summary>
        /// <returns></returns>
        public List<OCTeamClass> OCTeamClass_Cache_List()
        {
            ICache cache = CacheFactory.Create();
            if (!cache.Exists(string.Empty, "OCTeamClass_Cache_List"))
            {
                List<OCTeamClass> octeamlist = OCTeamDAL.OCTeamClass_Cache_List();
                cache.Set(string.Empty, "OCTeamClass_Cache_List", octeamlist);
                return octeamlist;
            }
            else
            {
                return cache.Get<List<OCTeamClass>>(string.Empty, "OCTeamClass_Cache_List");
            }
        }

        /// <summary>
        /// 获取在线课程教学团队列表
        /// </summary>
        /// <param name="OCID"></param>
        /// <returns></returns>
        public List<OCTeam> OCTeam_List(int OCID)
        {
            return OCTeamDAL.OCTeam_List(OCID);
        }
        public List<OCTeamClass> OCTeamClass_List(OCTeamClass model)
        {
            return OCTeamDAL.OCTeamClass_List(model);
        }

        public List<OCTeam> OCTeam_OCOwner_List(int userid)
        {
            return OCTeamDAL.OCTeam_OCOwner_List(userid);
        }





        #endregion

        #region  详细信息

        public OCTeamInfo TeacherInfo_Get(int UserID)
        {
            return OCTeamDAL.TeacherInfo_Get(UserID);
        }

        public OcTeamFunctionInfo OCTeam_Class_Function_Get(int OCID, int UserID)
        {
            return OCTeamDAL.OCTeam_Class_Function_Get(OCID, UserID);
        }

        #endregion

        #region  新增

        public OCTeam OCTeam_Class_Function_Save(OcTeamFunctionInfo model)
        {
            OCTeam octeam = new OCTeam();
            octeam.OCID = model.OCTeam.OCID;
            octeam.UserID = model.OCTeam.UserID;
            octeam.OCTeamClassIDs = ListHelp.GetPropertyValues(model.OcTeamFunctionClass.FindAll(oc => oc.IsSelected == true), "OCClassID");
            octeam.OCTeamModuleIDs = ListHelp.GetPropertyValues(model.OcTeamFunctionModule.FindAll(oc => oc.IsSelected == true), "ModuleID");
            
            octeam.ClassCount = model.OcTeamFunctionClass.FindAll(oc => oc.IsSelected == true).Count.ToString();

            octeam.FunctionCount = model.OcTeamFunctionModule.FindAll(oc => oc.IsSelected == true&&oc.ParentID!="0").Count.ToString();
            
            return OCTeamDAL.OCTeam_Class_Function_Save(octeam);
        }
        public OCTeam OCTeam_ADD(OCTeam model)
        {
            return OCTeamDAL.OCTeam_ADD(model);
        }

        public OCTeamInfo OCTeam_ADD(OCTeamInfo model)
        {
            return OCTeamDAL.OCTeam_ADD(model);
        }

        #endregion

        #region  对象更新
        public bool OCTeam_Role_Upd(OCTeam model)
        {
            return OCTeamDAL.OCTeam_Role_Upd(model);
        }
        public bool OCTeam_Brief_Upd(OCTeam model)
        {
            return OCTeamDAL.OCTeam_Brief_Upd(model);
        }
        public bool OCTeam_Status_Upd(int OCID, OCTeam model)
        {
            return OCTeamDAL.OCTeam_Status_Upd(OCID, model);
        }
        public bool OCTeam_IsLocked_Upd(OCTeam model)
        {
            return OCTeamDAL.OCTeam_IsLocked_Upd(model);
        }
        #endregion

        #region 单个批量更新
        #endregion

        #region 属性批量操作
        #endregion

        #region 删除

        public bool OCTeam_Del(OCTeam model)
        {
            return OCTeamDAL.OCTeam_Del(model);
        }
        #endregion







    }
}
