using CP.NLayer.Web.Mvc4.Common;
using System.Linq;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Controllers
{
    public class LanguageController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            var items = CultureHelper.DefaultSupportedCultures.Select(i => new SelectListItem
            {
                Text = i.NativeName,
                Value = i.Name,
                Selected = (i.Name == System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            });

            return PartialView(items);
        }

        [HttpPost]
        public ActionResult Change(string lang)
        {
            CookieHelper.Set(CookieKeys.UICulture, CultureHelper.GetSupportedCulture(lang, CultureHelper.DefaultSupportedCultures).Name, 3600);
            return new EmptyResult();
        }
    }
}