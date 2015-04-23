using Autofac;
using CP.NLayer.Resources.UI;
using CP.NLayer.Service.Contracts;
using System;
using System.Web.Mvc;

namespace CP.NLayer.Web.Mvc4.Common
{
    public class CrudController<TDisplayModel, TEditModel> : BaseController
        where TDisplayModel : class, new()
        where TEditModel : class, new()
    {
        protected IDisplayModelService<TDisplayModel> _displayModelService { get; private set; }
        protected IEditModelService<TEditModel> _editModelService { get; private set; }

        public CrudController(IDisplayModelService<TDisplayModel> displayModelService, IEditModelService<TEditModel> editModelService)
        {
            this._displayModelService = displayModelService;
            this._editModelService = editModelService;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult List()
        {
            if (Request.IsAjaxRequest())
            {
                // TODO: paging
                var m = _displayModelService.GetPage(0, 100);
                return PartialView(m);
            }
            else
            {
                return View("Index");
            }
        }

        public virtual ActionResult Create()
        {
            var m = _editModelService.Create();
            return ViewOrPartialView("Details", m);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Create(TEditModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _editModelService.Insert(m);
                    return AjaxSuccessOrRedirectTo();
                }
                catch (Exception ex)
                {
                    if (!HandleException(ex))
                    {
                        throw;
                    }
                }
            }

            return ViewOrPartialView("Details", m);
        }

        public virtual ActionResult Delete(long? id = null)
        {
            return Details(id);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(long? id = null)
        {
            if (id == null || id <= 0)
            {
                return ErrorView(UiResources.Error_ObjectNotFound);
            }
            try
            {
                _editModelService.DeleteById(id.Value);
                return AjaxSuccessOrRedirectTo();
            }
            catch (Exception ex)
            {
                if (HandleException(ex))
                {
                    return ErrorView(ex.Message);
                }
                throw;
            }
        }

        //[HttpPost]
        //public virtual ActionResult DeleteSelected(string[] ids)
        //{
        //    if (ids == null || ids.Count() == 0)
        //    {
        //        return ErrorView(UiResources.Error_ObjectNotFound);
        //    }
        //    try
        //    {
        //        ids.ToList().ForEach(x => _editModelService.DeleteById(x));
        //        return AjaxSuccessOrRedirectTo();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (HandleException(ex))
        //        {
        //            return ErrorView(ex.Message);
        //        }
        //        throw;
        //    }
        //}

        public virtual ActionResult Edit(long? id)
        {
            return Details(id);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEditModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // use new lifetime scope so as not to affect following dbContext in case we have failed updated entity in context
                    // or, we can detach the model in catch block
                    using (var scope = DependencyInjection.Container.BeginLifetimeScope())
                    {
                        var serv = scope.Resolve<IEditModelService<TEditModel>>();
                        serv.Update(m);
                    }
                    return AjaxSuccessOrRedirectTo();
                }
                catch (Exception ex)
                {
                    if (!HandleException(ex))
                    {
                        throw;
                    }
                }
            }

            return ViewOrPartialView("Details", m);
        }

        public virtual ActionResult Details(long? id = null)
        {
            if (id == null || id <= 0)
            {
                return ErrorView(UiResources.Error_ObjectNotFound);
            }
            var m = _editModelService.GetById(id.Value);
            if (m == null)
            {
                return ErrorView(UiResources.Error_ObjectNotFound);
            }
            return ViewOrPartialView("Details", m);
        }
    }
}