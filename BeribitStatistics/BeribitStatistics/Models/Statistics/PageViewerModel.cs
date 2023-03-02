using System;

namespace BeribitStatistics.Models.Statistics
{
    public class PageViewerModel
    {
        public string UserId { get; set; }
        public string Url { get; set; }
        public DateTime DateVisited { get; set; }

        public PageViewerModel(string userId, string url, DateTime dateVisited)
        {
            UserId = userId;
            Url = url;
            DateVisited = dateVisited;
        }
    }
}