using MambaExamAB103Saleh.DAL;
using MambaExamAB103Saleh.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaExamAB103Saleh.Areas.MambaAdmin.Controllers
{
    [Area("MambaAdmin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Setting> settings = _context.Settings.ToList();
            return View(settings);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);

            if (setting == null) return NotFound();

            return View(setting);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Setting setting)
        {
            if (id == null || id < 1) return BadRequest();

            Setting existed = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();

            existed.Value = setting.Value;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Setting existed = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);

            if (existed == null) return NotFound();

            _context.Settings.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
