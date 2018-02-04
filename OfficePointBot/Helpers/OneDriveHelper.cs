using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;

namespace OfficePointBot.Helpers
{
    public class OneDriveHelper
    {
        public async static Task<UserProfileInformation> GetOneDriveUserProfileAsync(string accessToken)
        {
            UserProfileInformation userProfile = null;

            using (var http = new HttpClient())
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var uri = new Uri("https://apis.live.net/v5.0/me");
 
                var stream = await client.GetStreamAsync(uri);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(UserProfileInformation));
                userProfile = (UserProfileInformation)ser.ReadObject(stream);                
            }

            return userProfile;
        }
    }
}