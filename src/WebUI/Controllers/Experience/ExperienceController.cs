using System.Threading;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.Experiences;
using LogOT.Application.Experiences.Commands;
using LogOT.Application.Experiences.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Experience;

public class ExperienceController : ControllerBaseMVC
{
    private readonly IApplicationDbContext _context;
    public ExperienceController(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> Index(Guid id)
    {
        var result = await Mediator.Send(new GetAllExperienceQuery(id));
        ViewBag.Id = id;
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add(Guid id)
    {
        var result = await Mediator.Send(new AddEmployeeExperienceCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Guid id, ExperienceDTO experience)
    {
        var result = await Mediator.Send(new AddEmployeeExperienceCommand(experience, id));
        if (ModelState.IsValid)
        {
            await _context.SaveChangesAsync(new CancellationToken());
            return RedirectToAction("Index", "Experience", new { id });
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var result = await Mediator.Send(new UpdateEmployeeExpericenCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, ExperienceDTO experience)
    {
        var result = await Mediator.Send(new UpdateEmployeeExpericenCommand(experience, id));
        if (ModelState.IsValid)
        {
            await _context.SaveChangesAsync(new CancellationToken());
            return RedirectToAction("Index", "Experience", new { id });
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await Mediator.Send(new DeleteEmployeeExperienceCommand(id));
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, ExperienceDTO experience)
    {
        var result = await Mediator.Send(new DeleteEmployeeExperienceCommand(id, experience));
        return RedirectToAction("Index", "Experience", new { id });
    }
}