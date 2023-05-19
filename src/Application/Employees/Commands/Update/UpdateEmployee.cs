using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;
//using NToastNotify;

namespace LogOT.Application.Employees.Commands.Update;
public record UpdateEmployee : IRequest
{
    public Guid Id { get; set; }
    public string ApplicationUserId { get; set; }

    public string IdentityNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string BankName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }

    public DateTime LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}

public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployee request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employee
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }


        entity.IdentityNumber = request.IdentityNumber;
        entity.BirthDay = request.BirthDay;
        entity.BankName = request.BankName;
        entity.BankAccountNumber = request.BankAccountNumber;
        entity.BankAccountName = request.BankAccountName;
        entity.Created = request.Created;
        entity.CreatedBy = request.CreatedBy;
        entity.LastModified = entity.LastModified;
        entity.LastModifiedBy = request.LastModifiedBy;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
