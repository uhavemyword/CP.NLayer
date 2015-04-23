// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services.Tests
{
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///This is a test class for UserServiceTest and is intended to contain all UserServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserServiceTest
    {
        [Ignore]
        [TestMethod()]
        public void LoginTest()
        {
            var userService = DependencyInjection.Container.Resolve<IUserService>();
            var user = userService.Login("user1", "a");
            Assert.IsNotNull(user);
            user = userService.Login("user1", "b");
            Assert.IsNull(user);
        }
    }
}