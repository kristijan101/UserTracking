using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserTracking.Common;
using UserTracking.Service.Common;
using UserTracking.Web.Models;

namespace UserTracking.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUserActivityLogReader userActivityLogReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userActivityLogReader">The user activity log reader.</param>
        public AdminController(IUserActivityLogReader userActivityLogReader)
        {
            this.userActivityLogReader = userActivityLogReader ?? throw new ArgumentNullException(nameof(userActivityLogReader));
        }

        public async Task<ActionResult> Index()
        {
            var activities = await this.userActivityLogReader.ReadAsync(new Pagination(1));
            return View(activities.Select(a => new UserActivityViewModel()
            {
                ActivityDate = a.ActivityDate,
                IPAddress = a.IPAddress,
                UserAgent = a.UserAgent,
                UserName = a.UserName,
                ViewCount = a.ViewCount
            }));
        }
    }
}