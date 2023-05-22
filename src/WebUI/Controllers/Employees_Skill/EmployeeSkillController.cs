using LogOT.Application.Common.Interfaces;
using LogOT.Application.Employees_Skill;
using LogOT.Application.Employees_Skill.Commands;
using LogOT.Application.Employees_Skill.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Employees_Skill;

public class EmployeeSkillController : ControllerBaseMVC
{
    private readonly IApplicationDbContext _context;

    public EmployeeSkillController(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(Guid id)
    {
        var result = await Mediator.Send(new GetEmployeeSkillQuery(id));
        ViewBag.Id = id;
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add(Guid id)
    {
        var result = await Mediator.Send(new AddEmployeeSkillCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Skill_EmployeeDTO skill_EmployeeDTO, Guid id)
    {
        var result = await Mediator.Send(new AddEmployeeSkillCommand(id, skill_EmployeeDTO));
        if (ModelState.IsValid)
        {
            await _context.SaveChangesAsync(new CancellationToken());
            return RedirectToAction("Index", new { id });
        }
        
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
        if (ModelState.IsValid)
        {
            await _context.SaveChangesAsync(new CancellationToken());
            return RedirectToAction("Index", new { id });
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteEmployeeSkillCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Skill_EmployeeDTO skill_EmployeeDTO, Guid id)
    {
        var result = await Mediator.Send(new DeleteEmployeeSkillCommand(id, skill_EmployeeDTO));
        if (result == null)
        {
            return NotFound();
        }
        return RedirectToAction($"Index", new { id });
    }
}