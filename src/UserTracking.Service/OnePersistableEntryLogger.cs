using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTracking.Model;
using UserTracking.Service.Common;

namespace UserTracking.Service
{
    public class OnePersistedEntryLogger : IUserActivityLogger
    {
        public Task LogAsync(UserActivity userActivity)
        {
            throw new NotImplementedException();
        }
    }
}
