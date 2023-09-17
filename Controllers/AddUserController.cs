using Microsoft.AspNetCore.Mvc;
using UniAuth.Models;

namespace UniAuth.Controllers
{
    public class AddUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult AddUser(HttpClient httpClient)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(Person person)
        {
            // Use _httpClientFactory to create an HttpClient instance
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync("http://test-api.softrig.com/api/biz/contacts", person);
            // Perform HTTP request with the HttpClient
            // You can send the 'person' data to your API endpoint here
            
            return RedirectToAction("MyContacts", "MyContacts"); 
        }
    }
}
