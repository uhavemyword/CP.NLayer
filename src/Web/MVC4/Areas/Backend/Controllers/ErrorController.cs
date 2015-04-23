using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Backend/Error/

        public ActionResult Index()
        {
            return View();
        }
    }
}