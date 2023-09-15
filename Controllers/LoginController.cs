using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace UniAuth.Controllers
{
    public class loginController : Controller
    {

        private readonly ILogger<loginController> _logger;

        public loginController(ILogger<loginController> logger)
        {
            _logger = logger;
        }

        private readonly Client Client = new Client
    {
        clientId = "1ac832c7-5d3a-4cb0-98ae-57c1511d74d3",
        clientSecret = "ThisIsASecret"
    };


     [HttpGet]
    public IActionResult RedirectToAuthorization()
    {
         string clientId = Client.clientId;
        string redirectUri = "https://localhost:4200/MyContacts";
        string state = "open";
        string scope = "AppFramework Administrator openid profile offline_access";
        

        string authorizationUrl = $"https://test-login.softrig.com/connect/authorize?" +
            $"client_id={clientId}&" +
            $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
            "response_type=code&" +
            "prompt=login&" +
            $"scope={scope}&" +
            $"state={Uri.EscapeDataString(state)}";

            return Redirect(authorizationUrl);
    }

        [Route("MyContacts")]
        public IActionResult MyContacts(string code)
        {
            return RedirectToAction("GetToken", new { code });
            
        }


    [HttpGet]
    public async Task<IActionResult> GetToken([FromQuery(Name = "code")] string? code = null)
    {
        if (code == null)
        {
            return Ok("No code");
        }

        _logger.LogInformation(code);

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

        if (!result.IsSuccessStatusCode) return BadRequest(returnContent);

        var identityResponse = JsonConvert.DeserializeObject<IdentityResponse>(returnContent);

        return await GetDataFromCompany(identityResponse.access_token);
        
    }

    private async Task<IActionResult> GetDataFromCompany(string accessToken)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        try
        {

            var jwtSecurityToken = new JwtSecurityToken(accessToken);
            if (jwtSecurityToken.Payload.TryGetValue("AppFramework", out var baseUrl))
            {
                Console.WriteLine($"Base url is {baseUrl}");
                var apiResult = await httpClient.GetAsync(new Uri(baseUrl + "api/biz/companysettings"));

                return Ok(await apiResult.Content.ReadAsStringAsync());
            }
            else
            {
                return BadRequest($"Could not find claim on token");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return BadRequest("this is wrong");
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
