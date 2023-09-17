using Microsoft.AspNetCore.Mvc;
using UniAuth.Models;

namespace UniAuth.Controllers
{
    public class AddUserController : Controller
    {

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(Person person)
        {
            // Use _httpClientFactory to create an HttpClient instance
            var httpClient = new HttpClient();

            var response = await httpClient.PostAsJsonAsync("http://test-api.softrig.com/api/biz/contacts", person);
            // Perform HTTP request with the HttpClient
            // You can send the 'person' data to your API endpoint here
            
            return RedirectToAction("MyContacts", "MyContacts"); 
        }
    }
}
