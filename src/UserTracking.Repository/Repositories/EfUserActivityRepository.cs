using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;
using UserTracking.Repository.Common;

namespace UserTracking.Repository.Repositories
{
    public class EFUserActivityRepository : IUserActivityRepository
    {
        public Task CreateAsync(UserActivity userActivity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserActivity>> GetUserActivitiesAsync(Pagination pagination)
        {
            throw new NotImplementedException();
        }

        public Task<UserActivity> GetUserActivityAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserActivity userActivity)
        {
            throw new NotImplementedException();
        }
    }
}
