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
        //Employee Id
        if (command.EmployeeId == null)
        {
            ModelState.AddModelError(nameof(command.EmployeeId), "EmployeeId is required.");
        }
        //Start Date
        if (command.StartDate == null)
        {
            ModelState.AddModelError(nameof(command.StartDate), "Start date is required.");
        }
        else if (command.StartDate < DateTime.Now)
        {
            ModelState.AddModelError(nameof(command.StartDate), "Start date must be equal to or greater than today");
        }
        //End Date
        if (command.EndDate == null)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date is required.");
        }
        else if (command.StartDate >= command.EndDate)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date must be greater than Start date");
        }
        //Reason
        if (command.Reason == null)
        {
            ModelState.AddModelError(nameof(command.Reason), "Reason is required.");
        }
        if (ModelState.IsValid)
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
        //Id
        if (command.Id == null)
        {
            ModelState.AddModelError(nameof(command.Id), "Id is required.");
        }
        //Start Date
        if (command.StartDate == null)
        {
            ModelState.AddModelError(nameof(command.StartDate), "Start date is required.");
        }
        else if (command.StartDate < DateTime.Now)
        {
            ModelState.AddModelError(nameof(command.StartDate), "Start date must be equal to or greater than today");
        }
        //End Date
        if (command.EndDate == null)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date is required.");
        }
        else if (command.StartDate >= command.EndDate)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date must be greater than Start date");
        }
        //Reason
        if (command.Reason == null)
        {
            ModelState.AddModelError(nameof(command.Reason), "Reason is required.");
        }
        //Status
        if (command.Status == null)
        {
            ModelState.AddModelError(nameof(command.Status), "Status type is required.");
        }
        if (ModelState.IsValid)
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
