using CP.NLayer.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Areas.Backend
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class BEAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string loginPage = "~/Backend/Login";
        private bool signedIn = false;

        public PermissionCodeEnum Permissions { get; set; }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                // If a child action cache block is active, we need to fail immediately, even if authorization
                // would have succeeded. The reason is that there's no way to hook a callback to rerun
                // authorization before the fragment is served from the cache, so we can't guarantee that this
                // filter will be re-run on subsequent requests.
                throw new InvalidOperationException("AuthorizeAttribute cannot be used within a child action caching block.");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                // auth failed(not signed in), redirect to login page
                if (!signedIn)
                {
                    filterContext.Result = new RedirectResult(loginPage + "?returnUrl=" + filterContext.HttpContext.Request.Url.AbsoluteUri, true);
                }
                else
                {
                    // auth failed(no permission), redirect to request page
                    filterContext.Result = new RedirectResult(filterContext.HttpContext.Request.Url.AbsoluteUri, true);
                }
            }
        }

        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var currentUser = BEUtility.GetCurrentUser();
            if (currentUser == null)
            {
                signedIn = false;
                return false;
            }

            signedIn = true;
            if (!currentUser.HasPermission(Permissions))
            {
                return false;
            }

            return true;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
    }
}