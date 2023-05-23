using LogOT.Application.Common.Interfaces;
using LogOT.Application.Holidays.Commands.CreateHoliday;
using LogOT.Application.Holidays.Commands.DeleteHoliday;
using LogOT.Application.Holidays.Commands.UpdateHoliday;
using LogOT.Application.Holidays.Queries.GetHoiliday;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers.Holiday;
public class HolidayController : ControllerBaseMVC
{
    private readonly IApplicationDbContext _context;

    public HolidayController(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ActionResult>  Index(GetHolidaysWithPaginationQuery getHolidaysWithPaginationQuery)
    {
        var result = await Mediator.Send(getHolidaysWithPaginationQuery);
        return View(result);
    }
    public IActionResult CreateHoliday()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> CreateHolidayView(CreateHolidayCommand createHolidayCommand)
    {
        var result = await Mediator.Send(createHolidayCommand);
        return RedirectToAction("Index");
    }

    public IActionResult Update(Guid id)
    {
        var holiday = _context.Holiday.FirstOrDefault(h => h.Id == id);
        if (holiday == null)
        {
            return NotFound();
        }

        var model = new UpdateHolidayCommand
        {
            Id = holiday.Id,
            DateName = holiday.DateName,
            Day = holiday.Day,
            HourlyPay = holiday.HourlyPay
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Update(UpdateHolidayCommand updateHolidayCommand)
    {
        /*if (!ModelState.IsValid)
        {
            return View(updateHolidayCommand);
        }*/

        await Mediator.Send(updateHolidayCommand);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteHolidayCommand = new DeleteHolidayCommand { Id = id };
        await Mediator.Send(deleteHolidayCommand);

        return RedirectToAction("Index");
    }



}
