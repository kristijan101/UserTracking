using System.Threading.Tasks;
using UserTracking.Model;

namespace UserTracking.Service.Common
{
    public interface IUserActivityLogger
    {
        /// <summary>
        /// Asynchronously adds or updates activity for a user.
        /// </summary>
        /// <param name="userActivity">The user activity.</param>
        /// <returns></returns>
        Task LogAsync(UserActivity userActivity);
    }
}
