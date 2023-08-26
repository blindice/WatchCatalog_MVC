using Microsoft.AspNetCore.Mvc;
using WatchCatalog_MVC.ViewModels;

namespace WatchCatalog_MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(ErrorViewModel viewModel)
        {
            return View(viewModel);
        }

        //public IActionResult PreviousPage()
        //{
        //    return Redirect(HttpContext.Request.Headers["Referer"]);
        //}
    }
}
