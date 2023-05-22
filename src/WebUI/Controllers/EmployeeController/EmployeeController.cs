using LogOT.Application.Employees.Queries.GetEmployee;
using LogOT.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.Employees.Commands;

namespace WebUI.Controllers.EmployeeController;
public class EmployeeController : ControllerBaseMVC
{


    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
    {
        var query = new GetEmployee
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

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


}

