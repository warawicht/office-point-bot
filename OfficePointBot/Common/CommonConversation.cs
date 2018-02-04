using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficePointBot.Common
{
    public static class CommonConversation
    {
        public static ConnectorClient Connector { get; set; }
        public static Activity CurrentActivity { get; set; }
    }
 
}
