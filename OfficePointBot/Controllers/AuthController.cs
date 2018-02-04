using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OfficePointBot.Controllers
{
    public class AuthController : ApiController
    {
        ICredentialStore _credentialStore;
        public AuthController()
        {
            ClientId = Common.CoreConstants.OneDriveClientId;
            ClientSecret = Common.CoreConstants.OneDriveClientSecret;

            _credentialStore = ConversationTokens.Store;
        }
              
        private static string RedirectUri = Common.CoreConstants.ReceiveTokenUrl;
        private readonly string ClientId;
        private static string Scopes = Common.CoreConstants.OneDriveScopes;
        private readonly string ClientSecret;

        [Route("api/auth/home")]
        [HttpGet]
        public HttpResponseMessage Home(string UserId)
        {
            var resp = Request.CreateResponse(System.Net.HttpStatusCode.Found);
            resp.Headers.Location = CreateOAuthCodeRequestUri(UserId);
            return resp;
        }

        private Uri CreateOAuthCodeRequestUri(string UserId)
        {
            UriBuilder uri = new UriBuilder("https://login.live.com/oauth20_authorize.srf");
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(RedirectUri));
            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(ClientId));
            query.AppendFormat("&client_secret={0}", Uri.EscapeUriString(ClientSecret));

            query.AppendFormat("&scope={0}", Uri.EscapeUriString(Scopes));
            query.Append("&response_type=code");
            if (!string.IsNullOrEmpty(UserId))
                query.Append($"&state={UserId}");

            uri.Query = query.ToString();

            return uri.Uri;
        }

        private Uri CreateOAuthTokenRequestUri(string code, string refreshToken = "")
        {
            UriBuilder uri = new UriBuilder("https://login.live.com/oauth20_token.srf");
            var query = new StringBuilder();

            query.AppendFormat("redirect_uri={0}", Uri.EscapeUriString(RedirectUri));
            query.AppendFormat("&client_id={0}", Uri.EscapeUriString(ClientId));
            query.AppendFormat("&client_secret={0}", Uri.EscapeUriString(ClientSecret));

            string grant = "authorization_code";
            if (!string.IsNullOrEmpty(refreshToken))
            {
                grant = "refresh_token";
                query.AppendFormat("&refresh_token={0}", Uri.EscapeUriString(refreshToken));
            }
            else
            {
                query.AppendFormat("&code={0}", Uri.EscapeUriString(code));
            }

            query.Append(string.Format("&grant_type={0}", grant));
            uri.Query = query.ToString();
            return uri.Uri;
        }

        [Route("api/auth/receivetoken")]
        [HttpGet()]
        public async Task<string> ReceiveToken(string code = null, string state = null)
        {
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(state))
            {
                var tokenUri = CreateOAuthTokenRequestUri(code);
                string result = null;

                using (var http = new HttpClient())
                {
                    var c = tokenUri.Query.Remove(0, 1);
                    var content = new StringContent(c);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    var resp = await http.PostAsync(new Uri("https://login.live.com/oauth20_token.srf"), content);
                    result = await resp.Content.ReadAsStringAsync();
                }

                dynamic obj = JsonConvert.DeserializeObject(result);
                var accessToken = obj.access_token.ToString();

                _credentialStore.AddToken(state, accessToken);

                UserProfileInformation userProfile = await Helpers.OneDriveHelper.GetOneDriveUserProfileAsync(accessToken);

                string firstName = userProfile.first_name;

                var reply = Common.CommonConversation.CurrentActivity.CreateReply("Welcome, " + firstName + ". How can I help you?");

                await Common.CommonConversation.Connector.Conversations.ReplyToActivityAsync(reply);
                
                return "Congratulations! You have been authenticated as " + firstName + " and can now return the Clippy for OfficePoint experience.";
            }

            return "Something went wrong - please try again!";
        }
    }
}
