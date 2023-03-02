using System.Linq;
using System.Threading.Tasks;
using BeribitStatistics.Models.Statistics;
using BeribitStatistics.Repositories;

namespace BeribitStatistics.Services
{
    public class StatisticService
    {
        private readonly StatisticRepository _repository;

        public StatisticService(StatisticRepository repository)
        {
            _repository = repository;
        }

        public async Task<ChartOnlineUsersModel> GetViewedPageStatistics(int days = 7)
        {
            var groupOnline = await _repository.GetViewedPageStatistics(days);

            return new ChartOnlineUsersModel(
                groupOnline.Keys.Select(d => d.ToString("dd-MM")).ToArray(),
                groupOnline.Values.ToArray());
        }
    }
}