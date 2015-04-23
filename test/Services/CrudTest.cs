// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.Services.Tests
{
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CrudTest
    {
        private const string _testPrefix = "Test";

        public CrudTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            DependencyInjection.Container.Resolve<ISystemService>().InitializeDatabase(true);

            // Insert test data
            var service = DependencyInjection.Container.Resolve<IRoleService>();
            for (int i = 0; i < 5; i++)
            {
                var role = new Role { Name = GenerateTestName() };
                service.Insert(role);
            }
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            var service = DependencyInjection.Container.Resolve<IRoleService>();
            var roles = service.GetAll().Where(x => x.Name.StartsWith(_testPrefix)).ToList();
            foreach (var role in roles)
            {
                service.Delete(role);
            }
        }

        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        private static string GenerateTestName()
        {
            return _testPrefix + Guid.NewGuid().ToString();
        }

        [TestMethod]
        public void InsertTest()
        {
            var service = DependencyInjection.Container.Resolve<IRoleService>();
            var oldValue = service.GetAll().Count();
            var role = new Role { Name = GenerateTestName() };
            service.Insert(role);
            var newValue = service.GetAll().Count();
            Assert.AreEqual<int>(oldValue + 1, newValue);
        }

        [TestMethod]
        public void UpdateTest()
        {
            string oldValue;
            string newValue;
            long id;
            var service = DependencyInjection.Container.Resolve<IRoleService>();
            var role = service.GetAll().Where(x => x.Name.StartsWith(_testPrefix)).FirstOrDefault();
            id = role.Id;
            oldValue = role.Name;
            do
            {
                newValue = GenerateTestName();
            }
            while (newValue == oldValue);
            role.Name = newValue;
            service.Update(role);

            var service2 = DependencyInjection.Container.Resolve<IRoleService>();
            var role2 = service2.GetById(id);
            Assert.AreNotEqual(oldValue, role2.Name);
            Assert.AreEqual(newValue, role2.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var service = DependencyInjection.Container.Resolve<IRoleService>();
            var oldValue = service.GetAll().Count();
            var role = service.GetAll().Where(x => x.Name.StartsWith(_testPrefix)).FirstOrDefault();
            service.Delete(role);
            var newValue = service.GetAll().Count();
            Assert.AreEqual<int>(oldValue - 1, newValue);
        }
    }
}