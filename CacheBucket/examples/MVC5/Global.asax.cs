using System;
using System.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.Data;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static bool EnableCacheBucket { get; private set; }

        protected void Application_Start()
        {

            var container = DependencyConfig.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DatabaseInitializer());

            EnableCacheBucket = "true".Equals(ConfigurationManager.AppSettings["EnableCacheBucket"], StringComparison.OrdinalIgnoreCase);
        }
    }
}
