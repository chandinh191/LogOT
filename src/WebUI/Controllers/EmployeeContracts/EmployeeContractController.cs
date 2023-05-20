using LogOT.Application.EmployeeContracts.Commands.CreateEmployeeContract;
using LogOT.Application.EmployeeContracts.Commands.DeleteEmployeeContract;
using LogOT.Application.EmployeeContracts.Commands.UpdateEmployeeContract;
using LogOT.Application.EmployeeContracts.Queries.GetEmployeeContract;
using LogOT.Application.EmployeeContracts.Queries.GetListEmployeeContract;
using LogOT.Application.TodoLists.Commands.DeleteTodoList;
using LogOT.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeContracts;
public class EmployeeContractController : ControllerBaseMVC
{
    [HttpGet]
    public async Task<IActionResult> Index(GetListEmployeeContractQuery query)
    {
        var Result = await Mediator.Send(query);
        return View(Result);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeContractCommand command)
    {
        if (ModelState.IsValid)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "EmployeeContract");
        }

        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        return View(command);
    }
    [HttpGet]
    public async Task<IActionResult> Update(Guid Id)
    {
        var Result = await Mediator.Send(new GetEmployeeContractByIdQuery(Id));
        return View(Result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateEmployeeContractCommand command)
    {
        if (ModelState.IsValid)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "EmployeeContract");
        }

        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        return View(command);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteEmployeeContractCommand(id));

        return RedirectToAction("Index", "EmployeeContract");
    }


}
