using BeribitStatistics.Models.Statistics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace BeribitStatistics.Services
{
    public class StatisticCashService
    {
        private static ConcurrentDictionary<string, PageViewerInfo> Viewers;
        private readonly ILogger<StatisticCashService> _logger;
        private List<PageViewerModel> HistoryViewers { get; }
        private List<UserIpAddressModel> IpAddresses { get; }

        public StatisticCashService(ILogger<StatisticCashService> logger)
        {
            _logger = logger;
            Viewers = new ConcurrentDictionary<string, PageViewerInfo>();
            HistoryViewers = new List<PageViewerModel>();
            IpAddresses = new List<UserIpAddressModel>();
        }

        public List<PageViewerModel> GetHistories()
        {
            var result = new List<PageViewerModel>(HistoryViewers);
            HistoryViewers.Clear();
            return result;
        }

        public List<UserIpAddressModel> GetIpAddresses()
        {
            var result = new List<UserIpAddressModel>(IpAddresses);
            IpAddresses.Clear();
            return result;
        }

        public void AddViewer(string userId, PageViewerInfo info, string ipAddress)
        {
            if (Viewers.ContainsKey(userId))
                RemoveViewer(userId, info.ConnectionId);

            if (Viewers.TryAdd(userId, info))
            {
                if (!HistoryViewers.Any(h => h.UserId.Equals(userId) && h.Url.Equals(info.Url)))
                    HistoryViewers.Add(new PageViewerModel(userId, info.Url, info.DateVisited));

                if (!IpAddresses.Any(h => h.UserId.Equals(userId) && h.IpAddress.Equals(ipAddress)))
                    IpAddresses.Add(new UserIpAddressModel(userId, ipAddress, DateTime.UtcNow));
            }
        }

        public void RemoveViewer(string userId, string connectionId)
        {
            if (!Viewers.ContainsKey(userId))
                return;

            var info = Viewers[userId];

            if (!info.ConnectionId.Equals(connectionId))
                return;

            if (!Viewers.TryRemove(userId, out info))
            {
                _logger.LogError($"{userId} don't remove info value");
            }
        }
    }
}