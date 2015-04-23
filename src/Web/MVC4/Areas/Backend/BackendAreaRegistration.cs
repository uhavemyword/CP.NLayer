using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend
{
    public class BackendAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Backend";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Backend_default",
                url: "Backend/{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "CP.NLayer.Web.Mvc4.Areas.Backend.Controllers" }
            );
        }
    }
}