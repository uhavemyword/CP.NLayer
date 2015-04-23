using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}