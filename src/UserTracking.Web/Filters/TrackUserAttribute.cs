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
            var userActivity = new UserActivity()
            {
                IPAddress = request.UserHostAddress,
                UserAgent = request.UserAgent
            };

            if (request.IsAuthenticated)
            {
                userActivity.UserId = filterContext.HttpContext.User.Identity.GetUserId();
                userActivity.UserName = filterContext.HttpContext.User.Identity.Name;
            }

            this.UserActivityLogger.WriteAsync(userActivity).Wait();
            base.OnActionExecuted(filterContext);
        }
    }
}