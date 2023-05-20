using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.LogOverTime.Commands.DeleteLogOverTime;
public record DeleteLogOverTimeCommand(Guid Id) : IRequest;
public class DeleteLogOverTimeCommandHandler : IRequestHandler<DeleteLogOverTimeCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteLogOverTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteLogOverTimeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLog
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

