using Microsoft.AspNetCore.Mvc;

namespace project_18.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
