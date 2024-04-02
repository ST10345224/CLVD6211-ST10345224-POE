using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraftPOE.Controllers
{
    public class MyWorkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
