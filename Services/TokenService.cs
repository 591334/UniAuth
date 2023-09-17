using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;
using UniAuth.Controllers;

namespace UniAuth.Services
{
    public class TokenService
    {

        }
        internal class IdentityResponse
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
            public string refresh_token { get; set; }
        }

    }
}
