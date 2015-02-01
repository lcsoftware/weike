using IES.Cache;
using IES.G2S.JW.DAL;
using IES.G2S.JW.IBLL;
using IES.JW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IES.CC.OC.Model;

namespace IES.G2S.JW.BLL
{
    public class UserBLL:IUserBLL
    {
        #region 列表
        public List<User> User_List(User model, int PageIndex, int PageSize)
        {
            return UserDAL.User_List( model, PageIndex, PageSize);
        }


        public List<OC> User_OC_List(IES.JW.Model.User model)
        {
            return UserDAL.User_OC_List(model);
        }

        public List<User> Student_List(User model, int PageIndex, int PageSize)
        {
            return UserDAL.User_List(model, PageIndex, PageSize);
        }
        public List<Teacher> Teacher_List(Teacher model, int PageIndex, int PageSize)
        {
            return UserDAL.Teacher_List(model, PageIndex, PageSize);
        }

        public List<User> User_DiskSpace_List(User model, int PageIndex, int PageSize)
        {
            return UserDAL.User_DiskSpace_List(model, PageIndex, PageSize);
        }

        // 根据组织机构ID获取学生列表
        public List<User> Student_Search(string key, int OrganizationID, int SpecialtyID, int ClassID, int IsRegister, string StudentIDs, int PageSize, int PageIndex)
        {
            return UserDAL.Student_Search(key,OrganizationID,SpecialtyID,ClassID,IsRegister,StudentIDs,PageSize,PageIndex);
        }

        // 根据组织机构ID获取教师列表
        public List<User> Teacher_Search(string Key, int OrganizationID, int PageSize, int PageIndex)
        {
            return UserDAL.Teacher_Search(Key, OrganizationID, PageSize, PageIndex);
        }

        #endregion

        #region 详细信息

        public User User_Get(User model)
        {

            ICache cache = CacheFactory.Create();

            if (!cache.Exists(model.UserID.ToString(), "User"))
            {
                User user = UserDAL.User_Get(model);
                cache.Set( user.UserID.ToString() , "User", user);
                return user ;
            }
            else
            {
                return cache.Get<User>(model.UserID.ToString(), "User");
            }
        }

        #endregion 

        #region 学生教师详细信息

        public User UserTS_Get(User model)
        {            
            return UserDAL.User_Get(model);
        }

        #endregion 

        #region  新增
        public User User_ADD(User model)
        {
            return UserDAL.User_ADD(model);
        }



        #endregion

        #region 删除
        public  bool User_Del(User model)
        {
            bool opt = UserDAL.User_Del(model);
            if (opt)
            {
                ICache cache = CacheFactory.Create();
                cache.SetExpire( model.UserID.ToString(), "User" );
            }
            return opt;
        }

        #endregion

        #region 批量删除
        public bool User_Batch_Del(string IDS)
        {
            return UserDAL.User_Batch_Del(IDS);
        }
        #endregion 

        #region 更新

        public  bool User_Upd(User model)
        {

             bool opt =  UserDAL.User_Upd(model);
             if (opt)
             {
                 ICache cache = CacheFactory.Create();
                 cache.Set(model.UserID.ToString(), "User", model );
             }
             return opt;
        }

        //修改密码
        public bool ChangePassword(User model)
        {
            return UserDAL.ChangePassword(model);
        }
        #endregion
    }
}
