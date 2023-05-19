using System.Security.Claims;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers;
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
            return RedirectToAction("Index", "Home");
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
        ClaimsPrincipal? result;
        try
        {
            result = await _identityService.AuthenticateAsync(model.Username.Trim(), model.Password.Trim());
            await HttpContext.SignInAsync(result);
            return Redirect("home");

        }
        catch (Exception ex)
        {
            return View();
        }
    }
}
