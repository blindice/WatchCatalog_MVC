using Microsoft.AspNetCore.Mvc;

namespace WatchCatalog_MVC.Controllers
{
    public class WatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
