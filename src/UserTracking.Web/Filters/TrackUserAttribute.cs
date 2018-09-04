using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using UserTracking.Model;
using UserTracking.Service.Common;

namespace UserTracking.Web.Filters
{
    public class TrackUserAttribute : ActionFilterAttribute
    {
        public IUserActivityLogger UserActivityLogger { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.IsAuthenticated)
            {
                LogUsersActivity();
            }

            base.OnActionExecuted(filterContext);

            void LogUsersActivity()
            {
                var userActivity = new UserActivity()
                {
                    IPAddress = request.UserHostAddress,
                    UserAgent = request.UserAgent,
                    Id = Guid.Parse(filterContext.HttpContext.User.Identity.GetUserId()),
                    UserName = filterContext.HttpContext.User.Identity.Name
                };

                this.UserActivityLogger.WriteAsync(userActivity).Wait();
            }
        }
    }
}