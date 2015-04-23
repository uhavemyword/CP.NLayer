using CP.NLayer.Common;
using CP.NLayer.Resources.UI;
using System;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Common
{
    public class BaseController : Controller
    {
        protected internal ActionResult ViewOrPartialView(string viewName = null, object model = null)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, model);
            }
            else
            {
                return View(viewName, model);
            }
        }

        protected internal ActionResult AjaxSuccessOrRedirectTo(RedirectToRouteResult result = null)
        {
            if (Request.IsAjaxRequest())
            {
                return Content(Consts.AjaxSuccess);
            }
            else
            {
                return result ?? RedirectToAction("Index");
            }
        }

        protected internal virtual bool HandleException(Exception ex)
        {
            bool handled = false;

            if (ex.IsDbConcurrencyException())
            {
                ModelState.AddModelError(string.Empty, UiResources.Error_DbUpdateConcurrency);
                handled = true;
            }
            else if (ex.IsUniqueConstraintViolation())
            {
                var uniqueError = ex.GetUniqueViolationInfo();
                if (uniqueError != null)
                {
                    ModelState.AddModelError(string.Empty, string.Format(UiResources.Error_DuplicateName_2, uniqueError.Item1, uniqueError.Item2));
                    handled = true;
                }
            }
            else if (ex is MyException)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                handled = true;
            }

            return handled;
        }

        protected internal ActionResult ErrorView(string message)
        {
            return ViewOrPartialView("Error", message ?? string.Empty);
        }
    }
}