using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.LogOverTime.Commands.UpdateLogOverTime;
public record UpdateLogOverTimeCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string? Status { get; set; }
    public bool IsDeleted { get; set; }
}
public class UpdateLogOverTimeCommandHandler : IRequestHandler<UpdateLogOverTimeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLogOverTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(UpdateLogOverTimeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLog
            .FindAsync(new object[] { request.EmployeeId }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.EmployeeId);
        }

        entity.EmployeeId = request.EmployeeId;
        entity.Date = request.Date;
        entity.Hours = request.Hours;
        entity.Status = request.Status;
        entity.IsDeleted = request.IsDeleted;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}