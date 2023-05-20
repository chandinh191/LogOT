using LogOT.Application.LeaveLogs.Commands.CreateLeaveLog;
using LogOT.Application.LeaveLogs.Commands.DeleteLeaveLog;
using LogOT.Application.LeaveLogs.Commands.UpdateLeaveLog;
using LogOT.Application.LeaveLogs.Queries.GetLeaveLog;
using LogOT.Application.LeaveLogs.Queries.GetListLeaveLog;
using LogOT.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.LeaveLogs;
public class LeaveLogController : ControllerBaseMVC
{
    [HttpGet]
    public async Task<IActionResult> Index(GetListLeaveLogByEmployeeIdQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    public IActionResult Create(Guid EmployeeId)
    {
        ViewBag.EmployeeId = EmployeeId;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateLeaveLogCommand command)
    {
        if(ModelState.IsValid)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index","LeaveLog", new { employeeId = "ac69dc8e-f88d-46c2-a861-c9d5ac894141" });
        }
        return View(command);
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid Id)
    {
        var result = await Mediator.Send(new GetLeaveLogByIdQuery(Id));
        return View(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateLeaveLogCommand command)
    {
        if(ModelState.IsValid)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "LeaveLog", new { employeeId = "ac69dc8e-f88d-46c2-a861-c9d5ac894141" });
        }
        return View(command);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Guid Id) 
    {
        await Mediator.Send(new DeleteLeaveLogCommand(Id));
        return RedirectToAction("Index", "LeaveLog", new { employeeId = "ac69dc8e-f88d-46c2-a861-c9d5ac894141" });
    }
}
