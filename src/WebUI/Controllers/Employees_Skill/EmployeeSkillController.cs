using LogOT.Application.Employees_Skill;
using LogOT.Application.Employees_Skill.Commands;
using LogOT.Application.Employees_Skill.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Employees_Skill;

public class EmployeeSkillController : ControllerBaseMVC
{
    public async Task<IActionResult> Index(Guid id)
    {
        var result = await Mediator.Send(new GetEmployeeSkillQuery(id));
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var result = await Mediator.Send(new UpdateEmployeeSkillCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Skill_EmployeeDTO skill_Employee, Guid id)
    {
        var result = await Mediator.Send(new UpdateEmployeeSkillCommand(id, skill_Employee));
        if (result != null)
        {
            return RedirectToAction($"Index", new {id});
        }
        return View(result);
    }
}