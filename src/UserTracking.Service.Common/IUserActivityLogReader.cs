using System.Threading.Tasks;
using UserTracking.Common;
using UserTracking.Model;

namespace UserTracking.Service.Common
{
    public interface IUserActivityLogReader
    {
        /// <summary>
        /// Asynchronously reads a log source.
        /// </summary>
        /// <param name="pagination">Paging options.</param>
        /// <returns></returns>
        Task<PagedResult<UserActivity>> ReadAsync(Pagination pagination, SortingParameters orders, string user = "");
    }
}
