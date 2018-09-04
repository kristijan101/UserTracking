using Autofac;
using UserTracking.Service.Common;
using UserTracking.Service.Services;

namespace UserTracking.Service.Infrastructure
{
    public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserActivityRepositoryReader>().As<IUserActivityLogReader>();
            builder.RegisterType<OneEntryPerUserLogger>().As<IUserActivityLogger>();
        }
    }
}
