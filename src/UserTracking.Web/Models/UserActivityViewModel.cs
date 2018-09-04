using System;
using System.ComponentModel;

namespace UserTracking.Web.Models
{
    public class UserActivityViewModel
    {
        [DisplayName("IP")]
        public string IPAddress { get; set; }

        [DisplayName("Last Activity")]
        public string ActivityDate { get; set; }

        [DisplayName("User Agent")]
        public string UserAgent { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Total Views")]
        public int ViewCount { get; set; }
    }
}