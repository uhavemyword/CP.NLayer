using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    [BEAuthorize()]
    public class MainController : BEController
    {
        //
        // GET: /Backend/Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}