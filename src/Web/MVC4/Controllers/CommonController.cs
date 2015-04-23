using CP.NLayer.Web.Mvc4.Common;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Controllers
{
    public class CommonController : Controller
    {
        [HttpPost]
        public ActionResult ValidateCaptcha(string action)
        {
            if (action == "qaptcha")
            {
                Session[SessionKeys.Captcha] = true;
                return Json(new { error = false });
            }
            else
            {
                Session[SessionKeys.Captcha] = null;
                return Json(new { error = true });
            }
        }
    }
}