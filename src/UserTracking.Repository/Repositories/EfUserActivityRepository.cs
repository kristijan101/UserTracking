using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;
using UserTracking.Repository.Common;
using UserTracking.Repository.Entities;
using UserTracking.Repository.EntityFramework;

namespace UserTracking.Repository.Repositories
{
    public class EFUserActivityRepository : IUserActivityRepository
    {

        public async Task CreateAsync(UserActivity userActivity)
        {
            var userActivityEntity = new UserActivityEntity()
            {
                ActivityDate = userActivity.ActivityDate,
                DateCreated = DateTime.UtcNow,
                Id = userActivity.Id,
                IPAddress = userActivity.IPAddress,
                UserAgent = userActivity.UserAgent,
                UserName = userActivity.UserName,
                ViewCount = userActivity.ViewCount
            };
            using (var dbContext = CreateContext())
            {
                dbContext.Set<UserActivityEntity>().Add(userActivityEntity);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<PagedResult<UserActivity>> GetUserActivitiesAsync(Pagination pagination, SortingParameters orders, string user = "")
        {
            if(pagination == null)
            {
                throw new ArgumentNullException(nameof(pagination));
            }
            if(orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            using (var dbContext = CreateContext())
            {
                IQueryable<UserActivityEntity> query = dbContext.Set<UserActivityEntity>().AsNoTracking();

                var totalCount = await query.CountAsync().ConfigureAwait(false);

                query = query.OrderBy(orders.GetSortExpression());

                if (!string.IsNullOrEmpty(user))
                {
                    query = query.Where(a => a.UserName.Contains(user));
                }

                var skipCount = (pagination.PageNumber - 1) * pagination.PageSize;
                if(skipCount > 0)
                {
                    query = query.Skip(skipCount);
                }
                query = query.Take(pagination.PageSize);

                var entities = await query.ToListAsync().ConfigureAwait(false);

                return new PagedResult<UserActivity>() { TotalRecords = totalCount, Items = entities.Select(e => CreateUserActivity(e)) };
            }
        }

        public async Task<UserActivity> GetUserActivityAsync(Guid userId)
        {
            using (var dbContext = CreateContext())
            {
                var entity = await dbContext.Set<UserActivityEntity>().FindAsync(userId).ConfigureAwait(false);
                return entity != null ? CreateUserActivity(entity) : null;
            }
        }

        public async Task UpdateAsync(UserActivity userActivity)
        {
            using (var dbContext = CreateContext())
            {
                var entity = await dbContext.Set<UserActivityEntity>().FindAsync(userActivity.Id).ConfigureAwait(false);
                entity.ActivityDate = userActivity.ActivityDate;
                entity.IPAddress = userActivity.IPAddress;
                entity.UserAgent = userActivity.UserAgent;
                entity.UserName = userActivity.UserName;
                entity.ViewCount = userActivity.ViewCount;
                await dbContext.SaveChangesAsync();
            }
        }

        protected virtual UserTrackingContext CreateContext()
        {
            return new UserTrackingContext();
        }

        private UserActivity CreateUserActivity(UserActivityEntity userActivityEntity)
        {
            return new UserActivity()
            {
                ActivityDate = userActivityEntity.ActivityDate,
                DateCreated = userActivityEntity.DateCreated,
                Id = userActivityEntity.Id,
                IPAddress = userActivityEntity.IPAddress,
                UserAgent = userActivityEntity.UserAgent,
                UserName = userActivityEntity.UserName,
                ViewCount = userActivityEntity.ViewCount
            };
        }
    }
}
