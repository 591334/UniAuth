using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using UniAuth.Models;
using UniAuth.Services;
using static System.Net.WebRequestMethods;

namespace UniAuth.Controllers
{
    public class loginController : Controller
    {

        private readonly Client Client = new Client
        {
            clientId = "1ac832c7-5d3a-4cb0-98ae-57c1511d74d3",
            clientSecret = "ThisIsASecret"
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedirectToAuthorization()
        {
            string clientId = Client.clientId;
            string redirectUri = "https://localhost:4200/MyContacts";
            string state = "open";
            string scope = "AppFramework profile openid offline_access";


            string authorizationUrl = $"https://test-login.softrig.com/connect/authorize?" +
                $"client_id={clientId}&" +
                $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                "response_type=code&" +
                "prompt=login&" +
                $"scope={scope}&" +
                $"state={Uri.EscapeDataString(state)}";

            return Redirect(authorizationUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetToken([FromQuery(Name = "code")] string code = null)
        {
            if (code == null)
            {
                return Ok("No Code");
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
            var returnContent = await result.Content.ReadAsStringAsync();


            var identityResponse = JsonConvert.DeserializeObject<IdentityResponse>(returnContent);

            if (identityResponse != null)
            {
                return Ok(identityResponse.access_token);
            }

            return BadRequest("Failed to obtain access token.");
        }

    } 

    internal class IdentityResponse
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
    }
    

    internal class Client
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }

}
