using CP.NLayer.Models.Business;
using CP.NLayer.Resources.UI;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Web.Mvc4.Common;
using Microsoft.Security.Application;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            if (BEUtility.GetCurrentUser() != null)
            {
                return RedirectToAction("", "");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (Request["iQapTcha"] == "" &&
                Session[SessionKeys.Captcha] != null &&
                (bool)Session[SessionKeys.Captcha] == true &&
                ModelState.IsValid
                )
            {
                var user = _userService.Login(model.UserName, model.Password);
                if (user != null && user.IsActive == true)
                {
                    if (model.RememberMe)
                    {
                        CookieHelper.Set(CookieKeys.UserId, user.Id.ToString());
                    }

                    BEUtility.Login(user);

                    returnUrl = Sanitizer.GetSafeHtmlFragment(returnUrl);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("", "");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, UiResources.Error_UserNameOrPasswordIncorrect);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}