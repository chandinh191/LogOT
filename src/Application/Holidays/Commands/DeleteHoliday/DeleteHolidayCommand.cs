using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.Holidays.Commands.CreateHoliday;
using LogOT.Application.Holidays.Commands.UpdateHoliday;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.Holidays.Commands.DeleteHoliday;
public class DeleteHolidayCommand : IRequest
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
}

public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteHolidayCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {

        var entity = await _context.Holiday.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Holiday), request.Id);
        }

        

        entity.IsDeleted= true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}
