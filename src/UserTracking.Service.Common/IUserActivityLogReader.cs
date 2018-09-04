using System.Collections.Generic;
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
        Task<IEnumerable<UserActivity>> ReadAsync(Pagination pagination);
    }
}
