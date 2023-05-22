using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.Holidays.Commands.UpdateHoliday;
public class UpdateHolidayCommand : IRequest
{
    public Guid Id { get; set; }
    public string DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }

}

public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Holiday.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Holiday), request.Id);
        }

        entity.DateName = request.DateName;
        entity.Day = request.Day;
        entity.HourlyPay = request.HourlyPay;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}
