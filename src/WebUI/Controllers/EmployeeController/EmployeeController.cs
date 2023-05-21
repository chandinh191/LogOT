using LogOT.Application.Employees.Queries.GetEmployee;
using LogOT.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.Employees.Commands.Create;
using LogOT.Application.Employees.Commands.Update;
using LogOT.Application.Employees.Commands.Delete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using LogOT.Domain.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebUI.Models;
using LogOT.Application.Common.Interfaces;
using Microsoft.Extensions.Options;


namespace WebUI.Controllers.EmployeeController;

public class EmployeeController : ControllerBaseMVC
{

    private readonly IIdentityService _identityService;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private ISender _mediator = null!;
    public readonly IWebHostEnvironment _environment;
    protected ISender Mediator => _mediator ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();
    public EmployeeController(IOptions<IdentityOptions> optionsAccessor, IIdentityService identityService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment)
    {
        _identityService = identityService;
        this.userManager = userManager;
        this.roleManager = roleManager;
        _signInManager = signInManager;
        _environment = environment;
    }
    
    public async Task<ActionResult<PaginatedList<EmployeeDTO>>> Index(GetEmployee query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
   
    public IActionResult Create()
    {
        
        // Fetch the ApplicationUserIds and assign them to ViewData
       

        return View();
    }


    [AllowAnonymous]
    [HttpPost("register")]
    [HttpPost("create")]

    public async Task<IActionResult> Create([FromForm] RegisterUser registerModel, CreateEmployee createModel, string selectedRole)
    {
        if (ModelState.IsValid && registerModel != null)
        {
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new AppIdentityRole()
                {                  
                    Name = "Administrator",
                    Description = "Can not perform CRUD operations",
                };
                var roleResult = await roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser
            {
                UserName = registerModel.UserName.Trim(),
                Address = registerModel.Address.Trim(),
                Image = registerModel.Image.Trim(),
                Email = registerModel.Email.Trim(),
                Fullname = registerModel.FullName.Trim(),
                
            };

            var result = await userManager.CreateAsync(user, registerModel.Password.Trim());

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, selectedRole).Wait();
                await _signInManager.SignInAsync(user, isPersistent: false);
                createModel.ApplicationUserId = user.Id; // Gán ID của user vừa tạo vào createModel
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        if (ModelState.IsValid && createModel != null)
        {
            var entityId = await Mediator.Send(createModel);
            return RedirectToAction(nameof(Index));
        }

        return View();
    }



    public async Task<IActionResult> Edit(Guid id)
    {
        var query = new GetEmployeeById { Id = id };
        var employee = await Mediator.Send(query);
        if (employee == null)
        {
            return NotFound();
        }

        var updateCommand = new UpdateEmployee
        {
            ApplicationUserId = employee.ApplicationUserId,
            IdentityNumber = employee.IdentityNumber,

            BirthDay = employee.BirthDay,

            BankName = employee.BankName,
            BankAccountNumber = employee.BankAccountNumber,
            BankAccountName = employee.BankAccountName,
        };

        return View(updateCommand);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, UpdateEmployee command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        try
        {
            await Mediator.Send(command);
            //_toastNotification.AddSuccessToastMessage("Nhân viên đã được sửa thành công.");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Xử lý lỗi và trả về thông báo lỗi nếu cần
            ModelState.AddModelError("", "An error occurred while updating the employee.");
            return View(command);
        }
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        var query = new GetEmployeeById { Id = id };
        var employee = await Mediator.Send(query);
        if (employee == null)
        {
            return NotFound();
        }

        var updateCommand = new DeleteEmployee
        {
            ApplicationUserId = employee.ApplicationUserId,
            IdentityNumber = employee.IdentityNumber,

            BirthDay = employee.BirthDay,

            BankName = employee.BankName,
            BankAccountNumber = employee.BankAccountNumber,
            BankAccountName = employee.BankAccountName,
            IsDeleted = employee.IsDeleted,

        };

        return View(updateCommand);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, DeleteEmployee command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        try
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Xử lý lỗi và trả về thông báo lỗi nếu cần
            ModelState.AddModelError("", "An error occurred while updating the employee.");
            return View(command);
        }
    }
}

