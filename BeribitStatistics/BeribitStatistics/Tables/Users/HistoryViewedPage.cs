using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeribitStatistics.Tables.Users
{
    public class HistoryViewedPage
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey(nameof(CustomUser))]
        public string UserId { get; set; }
        public CustomUser User { get; set; }

        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }

        public HistoryViewedPage(
            string userId,
            string url,
            DateTime createdAt)
        {
            UserId = userId;
            Url = url;
            CreatedAt = createdAt;
        }
    }
}