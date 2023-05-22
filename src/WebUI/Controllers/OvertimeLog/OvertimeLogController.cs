using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.OvertimeLogs.Queries;
using LogOT.Application.OvertimeLogs.Commands;
using LogOT.Application.Employees_Skill.Commands;
using LogOT.Domain.Entities;
using LogOT.Application.OvertimeLogs;
using NToastNotify;

namespace WebUI.Controllers.OvertimeLogController;
public class OvertimeLogController : ControllerBaseMVC
{
    private readonly IToastNotification _toastNotification;
    public OvertimeLogController(IToastNotification toastNotification)
    {
        _toastNotification = toastNotification;
    }
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

    public async Task<IActionResult> UpdateApprovedStatus(Guid Id)
    {
         await Mediator.Send(new UpdateApprovedStatusCommand(Id));
        _toastNotification.AddSuccessToastMessage("Approved");
        return RedirectToAction("ApprovedOvertimeLog");
    }

    public IActionResult UpdateCancelStatus(Guid Id)
    {
        Task<OvertimeLogDTO> result = Mediator.Send(new UpdateCancelStatusCommand(Id));
        _toastNotification.AddErrorToastMessage("Cancel");
        return RedirectToAction("CancelOvertimeLog");
    }
}
