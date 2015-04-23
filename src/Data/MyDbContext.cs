// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

    // TODO: DbContext is not thread-safe. May hit exception "The context cannot be used while the model is being created."
    [DbConfigurationType(typeof(MyDbConfiguration))]
    internal class MyDbContext : DbContext
    {
        public MyDbContext()
        {
            //this.EnableLog();

            this.Configuration.AutoDetectChangesEnabled = true;

            //Windows Communication Foundation (WCF) cannot directly serialize or deserialize proxies because the DataContractSerializer
            //can only serialize and deserialize known types, and proxy types are not known types. When you need to serialize POCO entities,
            //disable proxy creation or use the ProxyDataContractResolver class to serialize proxy objects as the original POCO entities.
            //To disable proxy creation, set the ProxyCreationEnabled property to false.
            //See http://msdn.microsoft.com/en-us/library/dd456853.aspx
            this.Configuration.ProxyCreationEnabled = true; // set true as we'll use ApplyDataContractResolverAttribute

            //When using POCO entities with the built-in features of Entity Framework, proxy creation must be enabled in order to use lazy loading.
            //So, with POCO entities, if ProxyCreationEnabled is false, then lazy loading won't happen even if LazyLoadingEnabled is set to true.
            //See http://stackoverflow.com/questions/9688966/ef-4-lazy-loading-without-proxies
            //Note in client side, there will be no lazyLoading if call service via WCF. But, the serialization will access all properties of the entity, and load
            //all related object before sent the entity to WCF client.
            this.Configuration.LazyLoadingEnabled = false; // disable the lazy load to avoid serialization which will cause lazy load all properties.
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 6));

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .Map(x => x.MapLeftKey("UserId").MapRightKey("RoleId").ToTable("UserRoleMapping"));

            modelBuilder.Entity<Permission>()
                .HasMany(x => x.Roles)
                .WithMany(x => x.Permissions)
                .Map(x => x.MapLeftKey("PermissionId").MapRightKey("RoleId").ToTable("RolePermissionMapping"));

            //modelBuilder.Entity<User>().ToTable("User", schemaName: "Person");
            //modelBuilder.Entity<Department>().HasMany(x => x.Users).WithRequired(x => x.Department).HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(true);

            // Explicitly ignore the enum type as EF <=4.x not support enum type. Use correlative int instead.
            //modelBuilder.Entity<Role>().Ignore(x => x.Permission);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void EnableLog()
        {
            this.Database.Log = this.Log;
        }

        private void Log(string message)
        {
            Debug.Write(message);

            if (message.Contains("SELECT") && message.Contains("FROM"))
            {
                var linesCount = message.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Count();
                Debug.Write(string.Format("\r\n== Total lines count: {0}", linesCount));
                Debug.Assert(linesCount <= 2000, "===== The 'select from' script is more than 2000 lines. We need to improve the query! =====");
            }

            Regex regex = new Regex(@"Completed in (?<queryTime>\d+) ms with result", RegexOptions.None);
            Match match = regex.Match(message);
            if (match.Success)
            {
                int queryTime = int.Parse(match.Groups["queryTime"].Value);
                Debug.Assert(queryTime <= 3000, "===== The execution time of the sql script is more than 3 seconds. We need to improve the query! =====");
            }
        }
    }
}