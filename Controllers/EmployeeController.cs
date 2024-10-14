using Microsoft.AspNetCore.Mvc;

namespace SampleNeo4J.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
