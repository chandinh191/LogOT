using LogOT.Application.Employees.Queries;
using LogOT.Application.Experiences.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Experience;

public class ExperienceController : ControllerBaseMVC
{
    public async Task<ActionResult> Index(Guid id)
    {
        var result = await Mediator.Send(new GetAllExperienceQuery(id));
        return View(result);
    }
}
