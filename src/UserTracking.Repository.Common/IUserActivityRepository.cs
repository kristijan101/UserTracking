using System.Collections.Generic;
using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;

namespace UserTracking.Repository.Common
{
    public interface IUserActivityRepository
    {
        /// <summary>
        /// Asynchronously gets user activities.
        /// </summary>
        /// <param name="pagination">Paging options.</param>
        /// <returns></returns>
        Task<IEnumerable<UserActivity>> GetUserActivities(Pagination pagination);

        /// <summary>
        /// Asynchronously gets user activities for a user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IEnumerable<UserActivity>> GetUserActivities(string userId);

        /// <summary>
        /// Asynchronously updates a user activity entry.
        /// </summary>
        /// <param name="userActivity">The user activity.</param>
        /// <returns></returns>
        Task UpdateAsync(UserActivity userActivity);

        /// <summary>
        /// Asynchronously creates a user activity entry.
        /// </summary>
        /// <param name="userActivity">The user activity.</param>
        /// <returns></returns>
        Task CreateAsync(UserActivity userActivity);
    }
}
