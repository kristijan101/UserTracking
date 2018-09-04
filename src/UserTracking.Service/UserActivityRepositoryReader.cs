using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;
using UserTracking.Repository.Common;
using UserTracking.Service.Common;

namespace UserTracking.Service
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
        public Task<IEnumerable<UserActivity>> ReadAsync(Pagination pagination)
        {
            return this.userActivityRepository.GetUserActivities(pagination);
        }
    }
}
