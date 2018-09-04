using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;
using UserTracking.Repository.Common;
using UserTracking.Service.Common;

namespace UserTracking.Service.Services
{
    public class UserActivityRepositoryReader : IUserActivityLogReader
    {
        private readonly IUserActivityRepository userActivityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserActivityRepositoryReader"/> class.
        /// </summary>
        /// <param name="userActivityRepository"></param>
        public UserActivityRepositoryReader(IUserActivityRepository userActivityRepository)
        {
            this.userActivityRepository = userActivityRepository ?? throw new ArgumentNullException(nameof(userActivityRepository));
        }

        /// <summary>
        /// Asynchronously reads a log source.
        /// </summary>
        /// <param name="pagination">Paging options.</param>
        /// <returns></returns>
        public Task<PagedResult<UserActivity>> ReadAsync(Pagination pagination, SortingParameters orders, string user = "")
        {
            if (pagination == null)
            {
                throw new ArgumentNullException(nameof(pagination));
            }
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }
            return this.userActivityRepository.GetUserActivitiesAsync(pagination, orders, user);
        }
    }
}
