using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficePointBot.Common
{
    public enum UserIntentType
    {
        None,
        RandomFact,
        PhoneCall,
        SalesBreakdown,
        RandomPassword,
        TrendingNews,
        JustBacon,
        TellJoke
    }

    public enum PhoneCallType
    {
        None,
        BillGates,
        Daenerys,        
    }
}