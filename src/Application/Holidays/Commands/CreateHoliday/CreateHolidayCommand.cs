using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.TodoLists.Commands.CreateTodoList;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.Holidays.Commands.CreateHoliday;
public class CreateHolidayCommand : IRequest<Guid>
{
    public Guid CompanyId { get; set; }
    public string DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }

}

public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {

        var entity = new Holiday();

        entity.CompanyId = Guid.Parse("3d12e320-cd68-4ec0-b0b3-b1358b684449");
        entity.DateName = request.DateName;
        entity.Day = request.Day;
        entity.HourlyPay = request.HourlyPay;

        _context.Holiday.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
