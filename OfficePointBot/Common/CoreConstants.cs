using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfficePointBot.Common
{
    public class CoreConstants
    {
        public const string LuisSubscriptionKey = "[YOUR LUIS SUBSCRIPTION ID]";
        public const string LuisAppId = "[YOUR LUIS APP ID]";

        public const string OneDriveClientId = "[YOUR ONEDRIVE CLIENT ID]";
        public const string OneDriveClientSecret = "[YOUR ONEDRIVE CLIENT SECRET]";

        public const string OneDriveScopes = "wl.signin wl.basic onedrive.readwrite onedrive.appfolder wl.offline_access";

        public const string AuthenticationHostUrl = "http://localhost:3980";
        public const string ReceiveTokenUrl = "http://localhost:3980/api/auth/receivetoken";
        
        public const string FactServiceUrl = "[EMAIL SCOTT@LIQUIDDAFFODIL.COM TO GET AND USE THIS URL]";
    }
}