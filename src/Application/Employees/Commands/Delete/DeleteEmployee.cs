using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees.Commands.Delete;
public record DeleteEmployee : IRequest
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
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }
}

public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEmployee request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employee
               .Include(e => e.ApplicationUser) // Include the ApplicationUser
               .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        entity.IdentityNumber = request.IdentityNumber;
        entity.BirthDay = request.BirthDay;
        entity.BankName = request.BankName;
        entity.BankAccountNumber = request.BankAccountNumber;
        entity.BankAccountName = request.BankAccountName;
        entity.Created = request.Created;
        entity.CreatedBy = request.CreatedBy;
        entity.LastModified = request.LastModified;
        entity.LastModifiedBy = request.LastModifiedBy;
        entity.IsDeleted = true;

        // Update ApplicationUser properties
        if (entity.ApplicationUser != null)
        {
            entity.ApplicationUser.Fullname = request.Fullname;
            entity.ApplicationUser.Address = request.Address;
            entity.ApplicationUser.Image = request.Image;
            // Other ApplicationUser properties
            // ...
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

