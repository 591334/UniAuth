using Microsoft.AspNetCore.Mvc;

namespace UniAuth.Controllers
{
    public class EditUserController : Controller
    {
        public IActionResult EditUser(string name)
        {
            // Get Token
            // Request user in api with same name as parameter
            return View();
        }
    }
}
