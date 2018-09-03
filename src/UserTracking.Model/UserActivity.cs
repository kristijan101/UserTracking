using System;

namespace UserTracking.Model
{
    public class UserActivity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string IPAddress { get; set; }

        public DateTime ActivityDate { get; set; }

        public string UserAgent { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }
    }
}
