using LogOT.Application.Common.Models;
using LogOT.Application.Employees;
using LogOT.Application.Employees.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Employee;

public class EmployeeController : ControllerBaseMVC
{
    public async Task<ActionResult<PaginatedList<EmployeeDTO>>> Index(GetAllEmployeeWithPaginationQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
}