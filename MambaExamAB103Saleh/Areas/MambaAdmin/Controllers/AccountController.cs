using MambaExamAB103Saleh.Models;
using MambaExamAB103Saleh.Utilities.Enums;
using MambaExamAB103Saleh.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MambaExamAB103Saleh.Areas.MambaAdmin.Controllers
{
    [Area("MambaAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser appUser = new AppUser()
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                UserName = registerVM.Username,
                Email = registerVM.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _signInManager.SignInAsync(appUser, isPersistent: false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existed = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail);
            if (existed == null)
            {
                existed = await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);
                if (existed == null)
                {
                    ModelState.AddModelError("", "Email, username or password is not correct");
                    return View();
                }
            }

            var result = await _signInManager.PasswordSignInAsync(existed, loginVM.Password, false, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email, username or password is not correct");
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
