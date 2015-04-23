// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Reflection;

    //DropCreateDatabaseAlways | DropCreateDatabaseIfModelChanges | CreateDatabaseIfNotExists
    internal class MyDbInitializer : DropCreateDatabaseAlways<MyDbContext>
    {
        private MyDbContext _context;

        protected override void Seed(MyDbContext context)
        {
            _context = context;
            this.InsertData();
        }

        private void InsertData()
        {
            //var items = new List<Department>
            //    {
            //        new Department() { Name = "Department 1", Description = "Department Description 1" },
            //        new Department() { Name = "Department 2", Description = "Department Description 10" }
            //    };
            //items.ForEach(i => _context.Departments.Add(i));
            //_context.SaveChanges();
        }
    }
}