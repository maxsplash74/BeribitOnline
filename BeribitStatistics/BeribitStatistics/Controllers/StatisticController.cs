using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BeribitStatistics.Extensions;
using BeribitStatistics.Models.Responses;
using BeribitStatistics.Services;
using Microsoft.Extensions.Logging;

namespace BeribitStatistics.Controllers
{
    [Route("/api/stat/")]
    public class StatisticController : Controller
    {
        private readonly StatisticService _statisticService;
        private readonly ILogger<StatisticController> _logger;

        public StatisticController(
            StatisticService statisticService, 
            ILogger<StatisticController> logger)
        {
            _statisticService = statisticService;
            _logger = logger;
        }

        [Route("online/{days}")]
        public async Task<IActionResult> GetStatisticOnline(int days)
        {
            try
            {
                var stat = await _statisticService.GetViewedPageStatistics(days);

                return this.JsonSuccess(stat);
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetStatisticOnline)} return exception: {e.Message}");

                return this.JsonError(ErrorResponse.InternalServerError);
            }
        }
    }
}