using MambaExamAB103Saleh.DAL;
using MambaExamAB103Saleh.Models;
using MambaExamAB103Saleh.Utilities.TeamExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaExamAB103Saleh.Areas.MambaAdmin.Controllers
{
    [Area("MambaAdmin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            return View(teams);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (team.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo bosh ola bilmez");
                return View();
            }

            if (team.Photo != null)
            {
                if (!team.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type uygun deyil");
                    return View();
                }

                if (!team.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File type uygun deyil");
                    return View();
                }

                team.Image = await team.Photo.CreateFile(_env.WebRootPath, "admin/images");

                await _context.AddAsync(team);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Team existed = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (existed == null) return NotFound();

            return View(existed);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Team team)
        {
            if (id == null || id < 1) return BadRequest();

            Team existed = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (existed == null) return NotFound();

            if (team.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo bosh ola bilmez");
                return View();  
            }

            if (team.Photo != null)
            {
                if (!team.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type uygun deyil");
                    return View();
                }

                if (!team.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File type uygun deyil");
                    return View();
                }

                existed.Image.DeleteFile(_env.WebRootPath, "admin/images");
                existed.Image = await team.Photo.CreateFile(_env.WebRootPath, "admin/images");
            }

            existed.Name = team.Name;
            existed.Profession = team.Profession;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Team existed = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);

            if (existed == null) return NotFound();

            existed.Image.DeleteFile(_env.WebRootPath, "admin/images");

            _context.Teams.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
