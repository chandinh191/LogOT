using MediatR;
using Microsoft.AspNetCore.Mvc;
using LogOT.Application.OvertimeLogs.Queries;
using LogOT.Application.OvertimeLogs.Commands;
using LogOT.Application.Employees_Skill.Commands;
using LogOT.Domain.Entities;
using LogOT.Application.OvertimeLogs;
using NToastNotify;
using LogOT.Application.LeaveLogs.Queries;
using LogOT.Application.HRStaffLeaveLogs.Queries;
using LogOT.Application.HRStaffLeaveLogs.Commands;

namespace WebUI.Controllers.HRStaffLeaveLogs;
public class HRStaffLeaveLogController : ControllerBaseMVC
{
    private readonly IToastNotification _toastNotification;
    public HRStaffLeaveLogController(IToastNotification toastNotification)
    {
        _toastNotification = toastNotification;
    }
    [HttpGet]
    public async Task<IActionResult> Index(GetAllLeaveLogsQuery query)
    {
        var result = await Mediator.Send(query);

        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> PendingOvertimeLog(PendingOvertimeLogQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> SolvedLeaveLog(SolvedLeaveLogQuery query)
    {
        var result = await Mediator.Send(query);

        return View(result);
    }
    [HttpGet]
    public async Task<IActionResult> NotSolvedLeaveLog(NotSolvedLeaveLogQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    public async Task<IActionResult> UpdateSolvedStatus(Guid Id)
    {
        await Mediator.Send(new UpdateSolvedStatusCommand(Id));
        _toastNotification.AddSuccessToastMessage("Solved");
        return RedirectToAction("SolvedLeaveLog");
    }
    public async Task<IActionResult> UpdateNotSolvedStatus(Guid Id)
    {
        await Mediator.Send(new UpdateNotSolvedStatusCommand(Id));
        _toastNotification.AddErrorToastMessage("Not Solved");
        return RedirectToAction("NotSolvedLeaveLog");
    }


}
