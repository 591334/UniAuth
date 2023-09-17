using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using UniAuth.Models;
using UniAuth.Services;

namespace UniAuth.Controllers
{
    public class MyContactsController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> MyContactsAsync()
        {
            TokenService tokenService = new TokenService();
            await tokenService.SetTokenAsync();
            IdentityResponse ir = new IdentityResponse();
            string access_token = ir.access_token;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            httpClient.DefaultRequestHeaders.Add("CompanyKey", "91aec9da-f8c0-4fdd-a2a4-a4aeb82275c3");


            List<string> itemList = new List<string>
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };
        
            return View(itemList);
        }
    }
}
