using CP.NLayer.Models.Entities;
using CP.NLayer.Web.Mvc4.Common;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    [BEAuthorize()]
    public class AccountController : BEController
    {
        //[ChildActionOnly]
        public ActionResult UserInfo()
        {
            var user = (User)Session[SessionKeys.User];
            return PartialView(user);
        }

        public void LogOff()
        {
            CookieHelper.Delete(CookieKeys.UserId);
            Session.Remove(SessionKeys.User);
            Response.Redirect("~/Backend/Login", true);
        }
    }
}