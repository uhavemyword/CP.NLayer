using Autofac.Integration.Mvc;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Web.Mvc4.Common;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CP.NLayer.Web.Mvc4
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(DependencyInjection.Container));
            DependencyResolver.Current.GetService<ISystemService>().InitializeDatabase(true);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string currentCulture = CookieHelper.Get(CookieKeys.UICulture);
            if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            }
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureHelper.GetSupportedCulture(currentCulture);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}