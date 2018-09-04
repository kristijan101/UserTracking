using Autofac;
using UserTracking.Service.Common;

namespace UserTracking.Service.Infrastructure
{
    public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserActivityRepositoryReader>().As<IUserActivityLogReader>();
            builder.RegisterType<OnePersistedEntryPerUserLogger>().As<IUserActivityLogger>();
        }
    }
}
