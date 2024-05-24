using Microsoft.AspNetCore.Mvc;


namespace Prk1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
