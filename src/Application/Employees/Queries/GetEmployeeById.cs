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

namespace LogOT.Application.Employees.Queries.GetEmployee;
public class GetEmployeeById : IRequest<EmployeeDTO>
{
    public Guid Id { get; set; }
}

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, EmployeeDTO>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeeByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDTO> Handle(GetEmployeeById request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employee
            .Include(e => e.ApplicationUser) // Include the ApplicationUser
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        /* var entity = await _context.Employee.FindAsync(new object[] { request.Id }, cancellationToken);
         */
        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        // Map entity to EmployeeDTO
        var employeeDto = new EmployeeDTO
        {
            Id = entity.Id,
            ApplicationUserId = entity.ApplicationUserId,
            IdentityNumber = entity.IdentityNumber,
            BirthDay = entity.BirthDay,
            BankName = entity.BankName,
            BankAccountNumber = entity.BankAccountNumber,
            BankAccountName = entity.BankAccountName,
            Created = entity.Created,
            CreatedBy = entity.CreatedBy,
            LastModified = entity.LastModified,
            LastModifiedBy = entity.LastModifiedBy,

            // Map ApplicationUser properties
            Fullname = entity.ApplicationUser.Fullname,
            Address = entity.ApplicationUser.Address,
            PhoneNumber = entity.ApplicationUser.PhoneNumber,
            Image = entity.ApplicationUser.Image
            // Other ApplicationUser properties
            // ...
        };

        return employeeDto;
    }
}
