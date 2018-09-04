using Autofac;
using UserTracking.Repository.Common;
using UserTracking.Repository.EntityFramework;
using UserTracking.Repository.Repositories;

namespace UserTracking.Repository.Infrastructure
{
    public class RepositoryDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EFUserActivityRepository>().As<IUserActivityRepository>();
            builder.RegisterType<UserTrackingContext>().AsSelf();
        }
    }
}
