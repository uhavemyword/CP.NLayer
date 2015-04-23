// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data.Tests
{
    using CP.NLayer.Models.Entities;
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
            MyDbUtility.InitializeDatabase(true);

            // Insert test data
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                for (int i = 0; i < 5; i++)
                {
                    var role = new Role { Name = GenerateTestName() };
                    roleRepository.Add(role);
                }
                worker.SaveChanges();
            }
        }

        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var roles = roleRepository.Table.Where(x => x.Name.StartsWith(_testPrefix)).ToList();
                foreach (var role in roles)
                {
                    roleRepository.Remove(role);
                }

                worker.SaveChanges();
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
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var oldCount = roleRepository.Table.Count();

                var role = new Role { Name = GenerateTestName() };
                roleRepository.Add(role);
                worker.SaveChanges();

                var newCount = roleRepository.Table.Count();
                Assert.AreEqual<int>(oldCount + 1, newCount);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            string oldValue;
            string newValue;
            long id;
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var role = roleRepository.Table.Where(x => x.Name.StartsWith(_testPrefix)).FirstOrDefault();
                id = role.Id;
                oldValue = role.Name;
                do
                {
                    newValue = GenerateTestName();
                }
                while (newValue == oldValue);
                role.Name = newValue;
                roleRepository.Update(role);
                worker.SaveChanges();
            }

            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var role = roleRepository.GetById(id);
                Assert.AreNotEqual(oldValue, role.Name);
                Assert.AreEqual(newValue, role.Name);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var preCount = roleRepository.Table.Count();

                var role = roleRepository.Table.Where(x => x.Name.StartsWith(_testPrefix)).FirstOrDefault();
                roleRepository.Remove(role);
                worker.SaveChanges();

                var postCount = roleRepository.Table.Count();
                Assert.AreEqual<int>(preCount - 1, postCount);
            }
        }

        [TestMethod]
        public void ConcurrencyTest()
        {
            using (var worker = DependencyInjection.Container.Resolve<IUnitOfWork>())
            {
                var roleRepository = worker.GetRepository<Role>();
                var role = roleRepository.Table.Where(x => x.Name.StartsWith(_testPrefix)).FirstOrDefault();
                var id = role.Id;
                role.Name = GenerateTestName();

                using (var worker2 = DependencyInjection.Container.Resolve<IUnitOfWork>())
                {
                    var roleRepository2 = worker2.GetRepository<Role>();
                    var role2 = roleRepository2.GetById(id);
                    role2.Name = GenerateTestName();
                    roleRepository2.Update(role2);
                    worker2.SaveChanges();
                }

                roleRepository.Update(role);
                bool error = false;
                try
                {
                    worker.SaveChanges();
                }
                catch (Exception e)
                {
                    error = true;
                    Console.WriteLine(e.Message);
                }

                Assert.IsTrue(error, "Test failed. Expect an exception occurs.");
            }
        }
    }
}