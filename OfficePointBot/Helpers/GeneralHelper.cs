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
    public class GeneralHelper
    {
        public async static Task<List<NewsInformation>> GetTrendingNewsAsync()
        {
            List<NewsInformation> newsResults = new List<NewsInformation>();

            const string apiKey = "[YOUR BING API SEARCH KEY]";
            string queryUri = "https://bingapis.azure-api.net/api/v5/news/search";
                         
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);  
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            string bingRawResponse;  

            TrendingNewsInformation bingJsonResponse = null;  

            try
            {
                bingRawResponse = await httpClient.GetStringAsync(queryUri);
                bingJsonResponse = JsonConvert.DeserializeObject<TrendingNewsInformation>(bingRawResponse);
            }
            catch (Exception ex)
            { 
            }

            newsResults = bingJsonResponse.value.ToList();
                        
            return newsResults;
        }

        public static List<string> PossibleJokeResponses
        {
            get
            {
                List<string> jokeResponses = new List<string>();

                jokeResponses.Add("hi");
                jokeResponses.Add("tell me a knock knock joke");
                jokeResponses.Add("who's there");

                return jokeResponses;
            }
        }

        public async static Task<string> GetRandomFactAsync()
        {
            string randomFact = "";

            using (var http = new HttpClient())
            {
                var client = new HttpClient();
                
                var uri = new Uri(Common.CoreConstants.FactServiceUrl);

                

                var stream = await client.GetStreamAsync(uri);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(string));
                randomFact = (string)ser.ReadObject(stream);
                
            }

            return randomFact;
        }
    }
}