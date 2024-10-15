using Microsoft.AspNetCore.Mvc;

namespace SampleNeo4J.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
