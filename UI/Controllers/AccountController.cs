using Application;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Models;


namespace UI.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var res = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                false
            );

            if (res.Succeeded)
            {
                Console.WriteLine("Login successful");

                // Get the user
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Check and assign the "student" role
                    if (!await _userManager.IsInRoleAsync(user, "student"))
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "student");

                        if (!roleResult.Succeeded)
                        {
                            // Handle role assignment failure (e.g., log an error)
                            ModelState.AddModelError(string.Empty, "Failed to assign student role.");
                            return View(model);
                        }
                    }

                    // Check and assign the "employee" role
                    if (!await _userManager.IsInRoleAsync(user, "employee"))
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "employee");

                        if (!roleResult.Succeeded)
                        {
                            // Handle role assignment failure (e.g., log an error)
                            ModelState.AddModelError(string.Empty, "Failed to assign employee role.");
                            return View(model);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError(string.Empty, "Email/password is invalid");
        }

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("index", "home");
    }
}