using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UserTracking.Repository.EntityFramework.Mappings;

namespace UserTracking.Repository.EntityFramework
{
    public class UserTrackingContext : DbContext
    {
        static UserTrackingContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<UserTrackingContext>());
        }

        public UserTrackingContext() : base("name=UserTrackingConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserActivityEntityMap());
        }
    }
}
