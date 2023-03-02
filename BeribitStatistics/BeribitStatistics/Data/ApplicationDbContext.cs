using BeribitStatistics.Tables.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeribitStatistics.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HistoryViewedPage> HistoryViewedPages { get; set; }
        public virtual DbSet<HistoryUserIpAddress> HistoryUserIpAddresses { get; set; }
    }
}
