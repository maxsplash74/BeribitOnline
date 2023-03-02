using System;
using System.Linq;
using System.Threading.Tasks;
using BeribitStatistics.Repositories;
using BeribitStatistics.Services;
using BeribitStatistics.Tables.Users;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BeribitStatistics.Jobs
{
    [DisallowConcurrentExecution]
    public class SaveHistoryJob : IJob
    {
        private readonly ILogger<SaveHistoryJob> _logger;
        private readonly StatisticCashService _statisticCashService;
        private readonly StatisticRepository _repository;

        public SaveHistoryJob(
            ILogger<SaveHistoryJob> logger, 
            StatisticCashService statisticCashService, 
            StatisticRepository repository)
        {
            _logger = logger;
            _statisticCashService = statisticCashService;
            _repository = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            { 
                var jobName = nameof(SaveHistoryJob);
                _logger.LogInformation("Начало задачи " + jobName);

                var historyModels = _statisticCashService.GetHistories();

                if (historyModels.Count > 0)
                {
                    var histories = historyModels
                        .Select(h => new HistoryViewedPage(h.UserId, h.Url, h.DateVisited));

                    await _repository.AddHistoryViewedPages(histories);
                }

                var ipAddresses = _statisticCashService.GetIpAddresses();

                if (ipAddresses.Count > 0)
                {
                    var histories = ipAddresses
                        .Select(h => new HistoryUserIpAddress(h.UserId, h.IpAddress, h.CreatedAt));

                    await _repository.AddHistoryIpAddresses(histories);
                }

                _logger.LogInformation("Окончание задачи " + jobName);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}