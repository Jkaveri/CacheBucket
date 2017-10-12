#region

using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CB.InMemory;
using WebApplication.Data;
using WebApplication.Helpers;

#endregion

namespace WebApplication
{
    public class DependencyConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // register all controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // register InMemoryCacheStorage
            builder.RegisterType<InMemoryCacheStorage>().SingleInstance();

            // register user reference
            builder.RegisterType<UserPreferenceCacheBucket>();

            builder.RegisterType<UserPreferenceHelper>();

            builder.RegisterType<ApplicationDbContext>().InstancePerRequest();

            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}