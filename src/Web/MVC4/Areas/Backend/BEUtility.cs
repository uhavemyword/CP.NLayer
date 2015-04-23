using CP.NLayer.Models.Entities;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Web.Mvc4.Common;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend
{
    public class BEUtility
    {
        public static void Login(User user)
        {
            user.LastLoginAt = DateTime.UtcNow;
            user.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
            DependencyResolver.Current.GetService<IUserService>().Update(user);

            // Explicit Loading of Related Data,
            // since the objectContext will be disposed by autofac per HttpRequest,
            // and we need to store roles in session which has no http state.
            user.Roles.ToList();
            HttpContext.Current.Session[SessionKeys.User] = user;
        }

        public static User GetCurrentUser()
        {
            if (HttpContext.Current.Session[SessionKeys.User] == null)
            {
                long uid;

                if (long.TryParse(CookieHelper.Get(CookieKeys.UserId), out uid))
                {
                    var user = DependencyResolver.Current.GetService<IUserService>().GetById(uid);

                    if (user != null && user.IsActive == true)
                    {
                        Login(user);
                    }
                    else
                    {
                        CookieHelper.Delete(uid.ToString());
                    }
                }
            }

            return (User)HttpContext.Current.Session[SessionKeys.User];
        }
    }
}