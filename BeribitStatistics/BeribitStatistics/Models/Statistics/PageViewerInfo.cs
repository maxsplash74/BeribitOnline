using System;

namespace BeribitStatistics.Models.Statistics
{
    public class PageViewerInfo
    {
        public string ConnectionId { get; set; }
        public string Url { get; set; }
        public DateTime DateVisited { get; set; }

        public PageViewerInfo(string connectionId, string url)
        {
            ConnectionId = connectionId;
            Url = url;
            DateVisited = DateTime.UtcNow;
        }
    }
}