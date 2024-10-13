using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
