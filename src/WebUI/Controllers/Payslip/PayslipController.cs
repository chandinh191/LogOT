
using LogOT.Application.Payslip;
using LogOT.Application.Payslip.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeePayslip;
public class PayslipController : ControllerBaseMVC
{
    [HttpGet]
    public async Task<IActionResult> Index(GetAllPayslipQuery query)
    {
        var result = await Mediator.Send(query);
        return View(result);
    }
    public  IActionResult Index(Guid Id)
    {
        Task<PayslipDTO> result = Mediator.Send(new GetEmployeePayslipQuery(Id));
        return View(result);
    }
}
