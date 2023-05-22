
using LogOT.Domain.Entities;
using LogOT.Application.Employees.Queries.GetEmployee;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.ApplicationController;
public class ApplicationUserController : ControllerBaseMVC
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    public ApplicationUserController(UserManager<ApplicationUser> userManager,

          SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;

        this.signInManager = signInManager;
    }
    

    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]

    public IActionResult SignIn(SignIn signIn)
    {
        if (ModelState.IsValid)
        {
            var result = signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, signIn.RememberMe, false).Result;
            if (result.Succeeded)
            {
                var user = userManager.FindByNameAsync(signIn.UserName).Result;
                if (userManager.IsInRoleAsync(user, "Manager").Result)
                {
                    TempData["SuccessMessage"] = "Sign in successfully as a Manager";
                    return RedirectToAction("List", "EmployeeManager");
                }
                else if (userManager.IsInRoleAsync(user, "User").Result)
                {
                    TempData["SuccessMessage"] = "Sign in successfully as a User";
                    return RedirectToAction("List", "EmployeeManager");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Details");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid User Details");
            }
        }
        return View(signIn);
    }
}
