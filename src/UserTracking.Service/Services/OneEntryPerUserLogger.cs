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

            var existingUserActivityLog = await this.userActivityRepository.GetUserActivityAsync(userActivity.Id).ConfigureAwait(false);
            if (existingUserActivityLog != null)
            {
                await UpdateActivity(userActivity, existingUserActivityLog).ConfigureAwait(false);
            }
            else
            {
                await CreateActivity(userActivity).ConfigureAwait(false);
            }
        }

        private async Task CreateActivity(UserActivity userActivity)
        {
            var utcNow = DateTime.UtcNow;
            userActivity.ActivityDate = utcNow;
            userActivity.DateCreated = utcNow;
            userActivity.ViewCount = 1;
            await this.userActivityRepository.CreateAsync(userActivity).ConfigureAwait(false);
        }

        private async Task UpdateActivity(UserActivity newActivity, UserActivity existingActivity)
        {
            existingActivity.ActivityDate = DateTime.UtcNow;
            existingActivity.IPAddress = newActivity.IPAddress;
            existingActivity.UserAgent = newActivity.UserAgent;
            existingActivity.ViewCount++;
            await this.userActivityRepository.UpdateAsync(existingActivity).ConfigureAwait(false);
        }
    }
}
