using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;
using UniAuth.Controllers;

namespace UniAuth.Services
{
    public class TokenService
    {
        private readonly Client Client = new Client
        {
            clientId = "1ac832c7-5d3a-4cb0-98ae-57c1511d74d3",
            clientSecret = "ThisIsASecret"
        };
        public async Task<string> SetTokenAsync([FromQuery(Name = "code")] string code = null)
        {
            if (code == null)
            {
                return ("No Code");
            }

            var httpClient = new HttpClient();

            var pairs = new List<KeyValuePair<string, string>>
            {
            new KeyValuePair<string, string>("client_id", Client.clientId),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("client_secret", Client.clientSecret),
            new KeyValuePair<string, string>("redirect_uri", "https://localhost:4200/MyContacts"),
            };


            var content = new FormUrlEncodedContent(pairs);
            var result = await httpClient.PostAsync(new Uri("https://test-login.softrig.com/connect/token"), content);
            var returnContent = result.Content.ReadAsStringAsync();


            var identityResponse = JsonConvert.DeserializeObject<IdentityResponse>(await returnContent);

            if (identityResponse != null)
            {
                return (identityResponse.access_token);
            }

            return ("Failed to obtain access token.");
        }

    }
        public class IdentityResponse
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
            public string refresh_token { get; set; }
        }

}

