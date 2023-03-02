namespace BeribitStatistics.Models.Statistics
{
    public class ChartOnlineUsersModel
    {
        public string[] Dates { get; set; }
        public int[] Counts { get; set; }

        public ChartOnlineUsersModel(string[] dates, int[] counts)
        {
            Dates = dates;
            Counts = counts;
        }
    }
}