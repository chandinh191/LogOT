using LogOT.Application.Employees.Queries.GetEmployee;
using LogOT.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.Employees.Commands.Create;
using LogOT.Application.Employees.Commands.Update;
using LogOT.Application.Employees.Commands.Delete;

namespace WebUI.Controllers.EmployeeController;
public class EmployeeController : ControllerBaseMVC
{



    public async Task<ActionResult<PaginatedList<EmployeeDTO>>> Index(GetEmployee query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployee command)
    {
        await Mediator.Send(command);
        return RedirectToAction(nameof(Index));
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

