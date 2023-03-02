using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeribitStatistics.Tables.Users
{
    public class HistoryUserIpAddress
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(CustomUser))]
        public string UserId { get; set; }
        public CustomUser User { get; set; }

        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        public HistoryUserIpAddress(
            string userId,
            string ipAddress,
            DateTime createdAt)
        {
            UserId = userId;
            IpAddress = ipAddress;
            CreatedAt = createdAt;
        }
    }
}