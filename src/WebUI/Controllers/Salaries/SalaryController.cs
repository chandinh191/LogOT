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
        if (command.Income <= 0)
        {
            ModelState.AddModelError(nameof(command.Income), "Income must be greater than 0.");
        }

        if (string.IsNullOrEmpty(command.InsuranceType))
        {
            ModelState.AddModelError(nameof(command.InsuranceType), "InsuranceType is required.");
        }
        else
        {
            if (command.InsuranceType == "official")
            {
                ModelState.Remove(nameof(command.CustomSalary));
            }
            else
            {
                if (command.CustomSalary <= 0)
                {
                    ModelState.AddModelError(nameof(command.CustomSalary), "CustomSalary must be greater than 0.");
                }
            }
        }

        if (command.Number_Of_Dependents < 0)
        {
            ModelState.AddModelError(nameof(command.Number_Of_Dependents), "Number_Of_Dependents must be greater than or equal to 0.");
        }

        if (string.IsNullOrEmpty(command.SalaryType))
        {
            ModelState.AddModelError(nameof(command.SalaryType), "SalaryType is required.");
        }

        if (ModelState.IsValid)
        {
            var result = await Mediator.Send(command);
            ViewBag.SalaryDto = result;
            return View(command);
        }

        return View(command);
    }
}
