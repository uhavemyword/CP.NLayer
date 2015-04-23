// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services
{
    using CP.NLayer.Common;
    using CP.NLayer.Data;
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class UserService : EntityService<User>, IUserService
    {
        #region Private fields

        private ICrypto _crypt;

        #endregion

        #region Ctor

        public UserService()
            : this(null)
        {
        }

        public UserService(IUnitOfWork worker)
            : base(worker)
        {
            this._crypt = DependencyInjection.Container.Resolve<ICrypto>();
        }

        #endregion

        public override IList<User> GetAll()
        {
            var list = Worker.GetRepository<User>().Table
                                .Include(x => x.Department)
                                .Include(x => x.Roles)
                                .ToList();
            return list;
        }

        #region IUserService

        public User Login(string userName, string password)
        {
            Guard.ThrowIfNull(() => new { username = userName, password });
            User user = Worker.GetRepository<User>().Table
                                    .Where(x => x.UserName.ToLower() == userName.ToLower() && x.IsActive)
                                    .Include(x => x.Roles.Select(y => y.Permissions))
                                    .FirstOrDefault();
            if (user != null && _crypt.IsMatch(password, user.PasswordHash, user.PasswordSalt.GetValue()))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}