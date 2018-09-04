using System;
using System.Linq;
using System.Threading.Tasks;
using UserTracking.Model;
using UserTracking.Repository.Common;
using UserTracking.Service.Common;

namespace UserTracking.Service
{
    public class OnePersistedEntryPerUserLogger : IUserActivityLogger
    {
        private readonly IUserActivityRepository userActivityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePersistedEntryPerUserLogger"/> class.
        /// </summary>
        /// <param name="userActivityRepository">The user activity repository.</param>
        public OnePersistedEntryPerUserLogger(IUserActivityRepository userActivityRepository)
        {
            this.userActivityRepository = userActivityRepository ?? throw new ArgumentNullException(nameof(userActivityRepository));
        }

        /// <summary>
        /// Asynchronously logs a user activity. If an entry for a user already exists, it is overwritten.
        /// </summary>
        /// <param name="userActivity">The user activity to log.</param>
        /// <returns></returns>
        public async Task LogAsync(UserActivity userActivity)
        {
            if(userActivity == null)
            {
                throw new ArgumentNullException(nameof(userActivity));
            }

            var userActivities = await this.userActivityRepository.GetUserActivities(userActivity.UserId);
            if (userActivities.Any())
            {
                await UpdateActivity(userActivity, userActivities.First());
            }
            else
            {
                await CreateActivity(userActivity);
            }
        }

        private async Task CreateActivity(UserActivity userActivity)
        {
            var utcNow = DateTime.UtcNow;
            userActivity.ActivityDate = utcNow;
            userActivity.DateCreated = utcNow;
            await this.userActivityRepository.CreateAsync(userActivity);
        }

        private async Task UpdateActivity(UserActivity sourceActivity, UserActivity targetActivity)
        {
            targetActivity.ActivityDate = DateTime.UtcNow;
            targetActivity.IPAddress = sourceActivity.IPAddress;
            targetActivity.UserAgent = sourceActivity.UserAgent;
            await this.userActivityRepository.UpdateAsync(targetActivity);
        }
    }
}
