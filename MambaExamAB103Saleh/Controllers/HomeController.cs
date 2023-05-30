using MambaExamAB103Saleh.DAL;
using MambaExamAB103Saleh.Models;
using Microsoft.AspNetCore.Mvc;

namespace MambaExamAB103Saleh.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            return View(teams);
        }
    }
}
