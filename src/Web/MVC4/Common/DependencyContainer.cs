using Autofac;
using Autofac.Integration.Mvc;
using CP.NLayer.Models.Business;
using CP.NLayer.Service.Contracts;
using CP.NLayer.Service.Services;

namespace CP.NLayer.Web.Mvc4.Common
{
    public sealed class DependencyInjection
    {
        private readonly static IContainer _container;

        static DependencyInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAll();
            _container = builder.Build();
        }

        public static IContainer Container
        {
            get { return _container; }
        }
    }

    public static class ContainerBuilderExtension
    {
        public static void RegisterAll(this ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // services layer
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDisplayModelService>().As<IUserDisplayModelService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDisplayModelService>().As<IDisplayModelService<UserDisplayModel>>().InstancePerLifetimeScope();
            builder.RegisterType<UserEditModelService>().As<IUserEditModelService>().InstancePerLifetimeScope();
            builder.RegisterType<UserEditModelService>().As<IEditModelService<UserEditModel>>().InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleDisplayModelService>().As<IRoleDisplayModelService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleDisplayModelService>().As<IDisplayModelService<RoleDisplayModel>>().InstancePerLifetimeScope();
            builder.RegisterType<RoleEditModelService>().As<IRoleEditModelService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleEditModelService>().As<IEditModelService<RoleEditModel>>().InstancePerLifetimeScope();

            // System
            builder.RegisterType<SystemService>().As<ISystemService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerLifetimeScope();
            //builder.RegisterType<X>().PropertiesAutowired();
        }
    }
}