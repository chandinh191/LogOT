using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;

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
        var entity = await _context.Employee.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        // Map entity to EmployeeDTO
        var employeeDto = new EmployeeDTO
        {
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
        };

        return employeeDto;
    }
}
