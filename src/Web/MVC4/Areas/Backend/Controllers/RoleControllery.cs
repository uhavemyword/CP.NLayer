using CP.NLayer.Models.Business;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Web.Mvc4.Common;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    [BEAuthorize()]
    public class RoleController : CrudController<RoleDisplayModel, RoleEditModel>
    {
        private IRoleService _roleService;

        public RoleController(IDisplayModelService<RoleDisplayModel> displayServ,
                               IEditModelService<RoleEditModel> editServ,
                               IRoleService roleService)
            : base(displayServ, editServ)
        {
            _roleService = roleService;
        }
    }
}