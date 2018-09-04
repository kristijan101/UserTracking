using System.Data.Entity.ModelConfiguration;
using UserTracking.Repository.Entities;

namespace UserTracking.Repository.EntityFramework.Mappings
{
    public class UserActivityEntityMap : EntityTypeConfiguration<UserActivityEntity>
    {
        public UserActivityEntityMap()
        {
            ToTable("UserActivity");

            HasKey(p => p.Id);

            Property(p => p.UserAgent)
                .IsOptional();

            Property(p => p.UserName)
                .IsRequired();

            Property(p => p.IPAddress)
                .IsOptional();

            Property(p => p.DateCreated)
                .IsRequired();

            Property(p => p.ActivityDate)
                .IsRequired();

            Property(p => p.ViewCount)
                .IsRequired();
        }
    }
}
