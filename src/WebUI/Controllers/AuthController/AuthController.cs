using System.Security.Claims;

using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers.ApplicationController;

public class AuthController : Controller
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<ApplicationUser> userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;
    private ISender _mediator = null!;
    public readonly IWebHostEnvironment _environment;
    protected ISender Mediator => _mediator ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();
    public AuthController(IOptions<IdentityOptions> optionsAccessor, IIdentityService identityService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment)
    {
        _identityService = identityService;
        this.userManager = userManager;
        //this.roleManager = roleManager;
        _signInManager = signInManager;
        _environment = environment;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpGet("login")]
    public IActionResult Login()
    {

        if (User.Identity.IsAuthenticated)
        {
            //_toastNotification.AddSuccessToastMessage("Bạn đã đăng nhập vào tài khoản!");
            return RedirectToAction("Index", "Employee");
        }
        else
        {
            return View();
        }

    }


    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginWithPassword model)
    {
        try
        {
            var result = await _identityService.AuthenticateAsync(model.Username.Trim(), model.Password.Trim());

            if (result.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                var roles = await userManager.GetRolesAsync(user);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (await userManager.IsInRoleAsync(user, "Manager"))
                {
                    TempData["SuccessMessage"] = "Signed in successfully as a Manager";
                    return RedirectToAction("Index", "Employee");
                }
                else if (await userManager.IsInRoleAsync(user, "Staff"))
                {
                    TempData["SuccessMessage"] = "Signed in successfully as a User";
                    return RedirectToAction("Index", "Employee");
                }
                else if (await userManager.IsInRoleAsync(user, "Employee"))
                {
                    TempData["SuccessMessage"] = "Signed in successfully as a User";
                    return RedirectToAction("Index", "Employee");
                }
            }

            ModelState.AddModelError("", "Invalid User Details");
        }
        catch (Exception ex)
        {
            // Handle the exception
        }

        return View();
    }

    
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpGet("register")]
    public IActionResult Register()
    {
        if (User.Identity.IsAuthenticated)
        {
            //_toastNotification.AddSuccessToastMessage("Bạn đã đăng nhập vào tài khoản!");
            return RedirectToAction("Index", "Employee");
        }
        else
        {
            return View();
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUser model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName.Trim(),
                Address = model.Address.Trim(),
                Image = model.Image.Trim(),
                Email = model.Email.Trim(),
                Fullname = model.FullName.Trim(),
                // Other user properties
            };

            var result = await userManager.CreateAsync(user, model.Password.Trim());

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        return View(model);
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpGet("logout")]
    public IActionResult Logout()
    {
       
            return View();      

    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [HttpPost("logout")]

    public async Task<IActionResult> Logout(int a)
    {
        await HttpContext.SignOutAsync(); // Đăng xuất người dùng

        return RedirectToAction("Login"); // Chuyển hướng người dùng đến trang đăng nhập
    }
}
