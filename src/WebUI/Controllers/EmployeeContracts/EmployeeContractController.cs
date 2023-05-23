using LogOT.Application.EmployeeContracts.Commands.CreateEmployeeContract;
using LogOT.Application.EmployeeContracts.Commands.DeleteEmployeeContract;
using LogOT.Application.EmployeeContracts.Commands.UpdateEmployeeContract;
using LogOT.Application.EmployeeContracts.Queries.GetEmployeeContract;
using LogOT.Application.EmployeeContracts.Queries.GetListEmployeeContract;
using LogOT.Application.Employees.Queries;
using LogOT.Application.TodoLists.Commands.DeleteTodoList;
using LogOT.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        var employees = await Mediator.Send(new GetAllEmployeeQuery());
        ViewBag.Employees = employees;
        var command = new CreateEmployeeContractCommand();
        return View(command);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeContractCommand command)
    {
        //Employee Id
        if (command.EmployeeId == null)
        {
            ModelState.AddModelError(nameof(command.EmployeeId), "EmployeeId is required.");
        }
        //File
        if (command.File == null)
        {
            ModelState.AddModelError(nameof(command.File), "File is required.");
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
        //Salary
        if (command.Salary <= 0)
        {
            ModelState.AddModelError(nameof(command.Salary), "Salary must be greater than 0.");
        }
        //Job
        if (string.IsNullOrEmpty(command.Job))
        {
            ModelState.AddModelError(nameof(command.Job), "Job is required.");
        }
        //Salary type
        if (command.SalaryType == null)
        {
            ModelState.AddModelError(nameof(command.SalaryType), "Salary type is required.");
        }
        //Contract type
        if (command.ContractType == null)
        {
            ModelState.AddModelError(nameof(command.ContractType), "Contract type is required.");
        }
        if (ModelState.IsValid)
        {
            await Mediator.Send(command);
            Toast.AddSuccessToastMessage("Create Successfully");
            return RedirectToAction("Index", "EmployeeContract");
        }

        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        var employees = await Mediator.Send(new GetAllEmployeeQuery());
        ViewBag.Employees = employees;
        Toast.AddWarningToastMessage("Please check the information entered");
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
        //Id
        if (command.Id == null)
        {
            ModelState.AddModelError(nameof(command.Id), "Id is required.");
        }
        //File
        if (command.File == null)
        {
            ModelState.AddModelError(nameof(command.File), "File is required.");
        }
        //End date
        if (command.EndDate == null)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date is required.");
        }
        else if(command.EndDate <= DateTime.Now)
        {
            ModelState.AddModelError(nameof(command.EndDate), "End date must be greater than today.");
        }

        //Salary
        if (command.Salary <= 0)
        {
            ModelState.AddModelError(nameof(command.Salary), "Salary must be greater than 0.");
        }
        //Job
        if (string.IsNullOrEmpty(command.Job))
        {
            ModelState.AddModelError(nameof(command.Job), "Job is required.");
        }
        //Status
        if (command.Status == null)
        {
            ModelState.AddModelError(nameof(command.Status), "Status type is required.");
        }
        //Salary type
        if (command.SalaryType == null)
        {
            ModelState.AddModelError(nameof(command.SalaryType), "Salary type is required.");
        }
        //Contract type
        if (command.ContractType == null)
        {
            ModelState.AddModelError(nameof(command.ContractType), "Contract type is required.");
        }
        if (ModelState.IsValid)
        {
            await Mediator.Send(command);
            Toast.AddSuccessToastMessage("Update Successfully");
            return RedirectToAction("Index", "EmployeeContract");
        }
        // Nếu dữ liệu không hợp lệ, trả về lại view để hiển thị lỗi
        Toast.AddWarningToastMessage("Please check the information entered");
        return View(command);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteEmployeeContractCommand(id));
        Toast.AddSuccessToastMessage("Delete Successfully");
        return RedirectToAction("Index", "EmployeeContract");
    }

}
