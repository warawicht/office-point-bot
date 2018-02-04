using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace OfficePointBot
{
    public interface ICredentialStore
    {
        string GetToken(string id);
        void AddToken(string id, string token);
    }

    public class CredentialStore : ICredentialStore
    {
        Dictionary<string, string> _idMap = new Dictionary<string, string>();
        public void AddToken(string id, string token)
        {
            _idMap[id] = token;
        }

        public string GetToken(string id)
        {
            string val = null;
            if (_idMap.TryGetValue(id, out val))
            {
                return val;
            }
            return null;
        }
    }

    public class ConversationTokens
    {
        public static ICredentialStore Store = new CredentialStore();
    }

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
