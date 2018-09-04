using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using UserTracking.Repository.Infrastructure;
using UserTracking.Service.Infrastructure;

namespace UserTracking.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<RepositoryDIModule>();
            builder.RegisterModule<ServiceDIModule>();

            builder.RegisterFilterProvider();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}