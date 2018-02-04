using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OfficePointBot.Helpers
{
    public static class InputHelper
    {
        public static async Task<UserInputInformation> ParseUserInput(string input)
        {
            UserInputInformation userInput = null;
            
            using (var client = new HttpClient())
            {
                string subscriptionKey = Common.CoreConstants.LuisSubscriptionKey;
                string appId = Common.CoreConstants.LuisAppId;

                string query = Uri.EscapeDataString(input);

                string uri = $"https://api.projectoxford.ai/luis/v1/application?id={appId}&subscription-key={subscriptionKey}&q={query}";

                try
                {
                    var response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    userInput = JsonConvert.DeserializeObject<UserInputInformation>(data);
                }
                catch (Exception ex)
                {

                }
                return userInput;
            }
        }
    }
}