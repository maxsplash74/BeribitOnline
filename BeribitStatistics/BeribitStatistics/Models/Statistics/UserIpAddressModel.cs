using System;

namespace BeribitStatistics.Models.Statistics
{
    public class UserIpAddressModel
    {
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserIpAddressModel(string userId, string ipAddress, DateTime createdAt)
        {
            UserId = userId;
            IpAddress = ipAddress;
            CreatedAt = createdAt;
        }
    }
}