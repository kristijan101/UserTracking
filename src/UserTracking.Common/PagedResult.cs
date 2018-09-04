using System.Collections.Generic;

namespace UserTracking.Common
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
