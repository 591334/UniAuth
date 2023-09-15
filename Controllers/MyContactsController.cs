using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UniAuth.Controllers
{
    public class MyContactsController : Controller
    {
        
        [HttpGet]
        public IActionResult MyContacts()
        {
            return View();
        } 


    }
}
