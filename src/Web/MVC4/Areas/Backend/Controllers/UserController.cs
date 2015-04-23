using CP.NLayer.Models.Business;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Web.Mvc4.Common;

namespace CP.NLayer.Web.Mvc4.Areas.Backend.Controllers
{
    [BEAuthorize()]
    public class UserController : CrudController<UserDisplayModel, UserEditModel>
    {
        private IUserService _userService;

        public UserController(IDisplayModelService<UserDisplayModel> displayServ,
                               IEditModelService<UserEditModel> editServ,
                               IUserService userService)
            : base(displayServ, editServ)
        {
            _userService = userService;
        }
    }
}