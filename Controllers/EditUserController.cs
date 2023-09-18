using Microsoft.AspNetCore.Mvc;

namespace UniAuth.Controllers
{
    public class EditUserController : Controller
    {
        public IActionResult EditUser(string name)
        {
            return View();
        }
    }
}
