using LogOT.Application.LogOverTime.Commands.CreateLogOverTime;
using LogOT.Application.LogOverTime.Commands.DeleteLogOverTime;
using LogOT.Application.LogOverTime.Commands.UpdateLogOverTime;
using LogOT.Application.LogOverTime.Queries.GetListLogOverTime;
using LogOT.Application.LogOverTime.Queries.GetLogOverTime;
using LogOT.Application.TodoLists.Commands.CreateTodoList;
using LogOT.Application.TodoLists.Queries.ExportTodos;
using LogOT.Application.TodoLists.Queries.GetTodos;
using LogOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.LogOverTime;
public class LogOverTimeController : ControllerBaseMVC
{
    [HttpGet]
    public async Task<IActionResult> Index(GetListOverTimeLogQuery query)
    {
        var Result = await Mediator.Send(query);
        return View(Result);
    }

    public async Task<IActionResult> Create(Guid EmployeeId)
    {
        ViewBag.EmployeeId = EmployeeId;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateLogOvertimeCommand command)
    {

        var result = await Mediator.Send(command);
        if (result != null)
        {

            return RedirectToAction("Index", "LogOverTime");
        }


        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        return View(command);
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid Id)
    {
        var Result = await Mediator.Send(new GetOverLogByIdQuery(Id));
        return View(Result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateLogOverTimeCommand command)
    {
        if (ModelState.IsValid)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "LogOverTime");
        }

        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        return View(command);
    }
    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteLogOverTimeCommand(id));

        return RedirectToAction("Index", "LogOverTime");
    }

}
