
namespace App.AngularMvc.DataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class UserProvider : System.Web.UI.Page
    {
        [WebMethod]
        public static int Login(string userName, string password)
        {
            return userName.Equals("test") && password.Equals("123") ? 1 : -1;            
        }

        [WebMethod]
        public static UserEntity Register(string userName, string password)
        {
            return new UserEntity() { Id = 1, UserName = userName, Password = password };
        }

        [WebMethod]
        public static IList<UserEntity> GetUserList(UserGroup userGroup)
        {
            IList<UserEntity> userList = new List<UserEntity>();
            for (int i = 0; i < 10; i++)
			{
                userList.Add(new UserEntity() { Id = i + 1, UserName = string.Format("user{0}", i + 1), Password = string.Format("pwd{0}", i + 1) });
			}
            return userList;
        }
    }

    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserGroup
    {
        public int Id { get; set; }
    }
}