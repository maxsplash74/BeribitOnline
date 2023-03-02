using System;
using System.Collections.Generic;
using BeribitStatistics.Tables.Enums;
using Microsoft.AspNetCore.Identity;

namespace BeribitStatistics.Tables.Users
{
    public class CustomUser : IdentityUser
    {
        public long? TgChatId
        {
            get;
            set;
        }

        public bool IsConnectedTg
        {
            get
            {
                return TgChatId.HasValue;
            }
        }

        public KycStatus? StatusKyc
        {
            get;
            set;
        }

        public bool IsVerifyKyc
        {
            get
            {
                return StatusKyc == KycStatus.VERIFY;
            }
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public List<HistoryViewedPage> HistoryViewedPages
        {
            get;
            set;
        }

        public List<HistoryUserIpAddress> HistoryUserIpAddresses
        {
            get;
            set;
        }

        public CustomUser()
        {
            TgChatId = null;
            StatusKyc = null;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
