using Microsoft.AspNetCore.Mvc;

namespace MambaExamAB103Saleh.Areas.MambaAdmin.Controllers
{
    [Area("MambaAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
