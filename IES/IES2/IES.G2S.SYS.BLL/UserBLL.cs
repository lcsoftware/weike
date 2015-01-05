using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IES.G2S.SYS.DAL;
using IES.SYS.Model;
using IES.AOP.G2S;
using IES.G2S.SYS.IBLL;
using IES.Cache;
using IES.Security;
using IES.CC.OC.Model;

namespace IES.G2S.SYS.BLL
{
    public class UserBLL:IUserBLL
    {
        #region 列表
        public  List<User> User_List(User model, int PageIndex, int PageSize)
        {
            return UserDAL.User_List(model, PageIndex, PageSize);
        }


        public List<OC> User_OC_List(IES.SYS.Model.User model)
        {
            return UserDAL.User_OC_List(model);
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

        #endregion
    }
}
