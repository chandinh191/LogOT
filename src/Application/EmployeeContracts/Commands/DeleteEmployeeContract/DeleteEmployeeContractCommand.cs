using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;

namespace LogOT.Application.EmployeeContracts.Commands.DeleteEmployeeContract;
public record DeleteEmployeeContractCommand(Guid Id) : IRequest;

public class DeleteEmployeeContractCommandHandler : IRequestHandler<DeleteEmployeeContractCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployeeContractCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeContract
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(EmployeeContract), request.Id);
        }

        entity.IsDeleted = true;
        entity.LastModified = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
