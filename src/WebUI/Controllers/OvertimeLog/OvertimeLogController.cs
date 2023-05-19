using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.OvertimeLogs.Queries;
using LogOT.Application.OvertimeLogs.Commands;
using LogOT.Application.Employees_Skill.Commands;
using LogOT.Domain.Entities;
using LogOT.Application.OvertimeLogs;

namespace WebUI.Controllers.OvertimeLogController;
public class OvertimeLogController : ControllerBaseMVC
{
    [HttpGet]

    public async Task<IActionResult> Index(GetAllOvertimeQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> AwaitingOvertimeLog(GetAwaitingOvertimeRequestQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> ApprovedOvertimeLog(GetApprovedOvertimeRequestQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> CancelOvertimeLog(GetCancelOvertimeRequestQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
   
    public IActionResult UpdateApprovedStatus(Guid Id)
    {
        Task<OvertimeLogDTO> result = Mediator.Send(new UpdateApprovedStatusCommand(Id));
        return RedirectToAction("ApprovedOvertimeLog");
    }
    public IActionResult UpdateCancelStatus(Guid Id)
    {
        Task<OvertimeLogDTO> result = Mediator.Send(new UpdateCancelStatusCommand(Id));
        return RedirectToAction("CancelOvertimeLog");
    }
}
