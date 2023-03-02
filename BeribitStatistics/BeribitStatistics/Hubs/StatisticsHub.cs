using BeribitStatistics.Models.Statistics;
using BeribitStatistics.Tables.Users;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using BeribitStatistics.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BeribitStatistics.Hubs
{
    [Authorize]
    public class StatisticsHub : Hub
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly StatisticCashService _statisticCashService;

        public StatisticsHub(
            UserManager<CustomUser> userManager,
            StatisticCashService statisticCashService)
        {
            _userManager = userManager;
            _statisticCashService = statisticCashService;
        }

        public async Task OnVisitedPage(string url)
        {
            var user = await _userManager.GetUserAsync(Context.User);

            var info = new PageViewerInfo(Context.ConnectionId, url);
            var feature = Context.Features.Get<IHttpConnectionFeature>();

            _statisticCashService.AddViewer(user.Id, info, feature.RemoteIpAddress?.ToString());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            _statisticCashService.RemoveViewer(user.Id, Context.ConnectionId);
        }
    }
}