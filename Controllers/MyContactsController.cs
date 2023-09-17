using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniAuth.Models;

namespace UniAuth.Controllers
{
    public class MyContactsController : Controller
    {

        [HttpGet]
        public IActionResult MyContacts()
        {
            return View();
        }

        [HttpGet]
        private async Task<IActionResult> GetListOfContacts()
        {
            string accessToken = IdentityResponse.access_token;
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Add("CompanyKey", "91aec9da-f8c0-4fdd-a2a4-a4aeb82275c3");

            try
            {
                var jwtSecurityToken = new JwtSecurityToken(accessToken);
                if (jwtSecurityToken.Payload.TryGetValue("AppFramework", out var baseUrl))
                {
                    Console.WriteLine($"Base url is {baseUrl}");
                    var apiResult = await httpClient.GetAsync(new Uri(baseUrl + "api/biz/contacts"));
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
}
