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
            return View();
        }

        public async Task<ActionResult> GetLogs()
        {
            var draw = Request.QueryString["draw"];
            var start = Request.QueryString["start"];
            var length = Request.QueryString["length"];
            var orderBy = Request.QueryString[$"columns[{Request.QueryString["order[0][column]"]}][name]"];
            var orderDirection = Request.QueryString["order[0][dir]"];
            var searchUser = Request.QueryString["search[value]"];

            var pageSize = string.IsNullOrEmpty(length) ? 0 : Convert.ToInt32(length);
            var skipCount = string.IsNullOrEmpty(start) ? 1 : Convert.ToInt32(start);
            var pageNumber = (skipCount % pageSize) + 1;
            var result = await this.userActivityLogReader.ReadAsync(new Pagination(pageNumber, pageSize), new SortingParameters(new[] { new SortingPair(orderBy, orderDirection) }), searchUser);
            var activities = result.Items.Select(a => new UserActivityViewModel()
            {
                ActivityDate = a.ActivityDate.ToString(),
                IPAddress = a.IPAddress,
                UserAgent = a.UserAgent,
                UserName = a.UserName,
                ViewCount = a.ViewCount
            });
            return Json(new { draw = draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = activities }, JsonRequestBehavior.AllowGet);
        }
    }
}