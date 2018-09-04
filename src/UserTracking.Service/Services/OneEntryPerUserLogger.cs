using System;
using System.Threading.Tasks;
using UserTracking.Model;
using UserTracking.Repository.Common;
using UserTracking.Service.Common;

namespace UserTracking.Service.Services
{
    public class OneEntryPerUserLogger : IUserActivityLogger
    {
        private readonly IUserActivityRepository userActivityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneEntryPerUserLogger"/> class.
        /// </summary>
        /// <param name="userActivityRepository">The user activity repository.</param>
        public OneEntryPerUserLogger(IUserActivityRepository userActivityRepository)
        {
            this.userActivityRepository = userActivityRepository ?? throw new ArgumentNullException(nameof(userActivityRepository));
        }

        /// <summary>
        /// Asynchronously logs a user activity. If an entry for a user already exists, it is overwritten.
        /// </summary>
        /// <param name="userActivity">The user activity to log.</param>
        /// <returns></returns>
        public async Task WriteAsync(UserActivity userActivity)
        {
            if(userActivity == null)
            {
                throw new ArgumentNullException(nameof(userActivity));
            }

            var existingUserActivityLog = await this.userActivityRepository.GetUserActivityAsync(userActivity.Id);
            if (existingUserActivityLog != null)
            {
                await UpdateActivity(userActivity, existingUserActivityLog);
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
            userActivity.ViewCount = 1;
            await this.userActivityRepository.CreateAsync(userActivity);
        }

        private async Task UpdateActivity(UserActivity sourceActivity, UserActivity targetActivity)
        {
            targetActivity.ActivityDate = DateTime.UtcNow;
            targetActivity.IPAddress = sourceActivity.IPAddress;
            targetActivity.UserAgent = sourceActivity.UserAgent;
            targetActivity.ViewCount = sourceActivity.ViewCount + 1;
            await this.userActivityRepository.UpdateAsync(targetActivity);
        }
    }
}
