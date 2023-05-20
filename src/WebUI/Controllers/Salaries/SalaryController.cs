using LogOT.Application.Salaries.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Salaries;
public class SalaryController : ControllerBaseMVC
{
    public IActionResult Index()
    {
        var command = new CreateSalaryCommand();
        return View(command);
    }

    [HttpPost]
    public async Task<IActionResult> Index(CreateSalaryCommand command)
    {
        var result = await Mediator.Send(command);
        ViewBag.SalaryDto = result;
        return View(command);
    }
}
