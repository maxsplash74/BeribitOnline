using BeribitStatistics.Tables.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using BeribitStatistics.Data;

namespace BeribitStatistics.Repositories
{
    public class StatisticRepository
    {
        private readonly ApplicationDbContext _context;

        public StatisticRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddHistoryViewedPages(IEnumerable<HistoryViewedPage> pages)
        {
            await _context.HistoryViewedPages.AddRangeAsync(pages);
            await _context.SaveChangesAsync();
        }

        public async Task AddHistoryIpAddresses(IEnumerable<HistoryUserIpAddress> ipAddresses)
        {
            await _context.HistoryUserIpAddresses.AddRangeAsync(ipAddresses);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<DateTime, int>> GetViewedPageStatistics(int days = 7)
        {
            return await _context.HistoryViewedPages
                .Where(p => p.CreatedAt.AddDays(days) >= DateTime.UtcNow)
                .GroupBy(s => s.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count(),
                }).ToDictionaryAsync(x => x.Type.Date, y => y.Count);
        }
    }
}