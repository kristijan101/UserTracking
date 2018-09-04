using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserTracking.Service.Common;

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

        public ActionResult Index()
        {
            return View();
        }

        public class UserActivityViewModel
        {

        }
    }
}