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

namespace LogOT.Application.EmployeeContracts.Commands.UpdateEmployeeContract;
public record UpdateEmployeeContractCommand : IRequest
{
    public Guid Id { get; set; }
    public string? File { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? Salary { get; set; }
    public EmployeeContractStatus? Status { get; set; }
    public SalaryType? SalaryType { get; set; }
    public ContractType? ContractType { get; set; }
}

public class UpdateEmployeeContractCommandHandler : IRequestHandler<UpdateEmployeeContractCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateEmployeeContractCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeContract
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(EmployeeContract), request.Id);
        }

        entity.File = request.File;
        entity.EndDate = request.EndDate;
        entity.Job = request.Job;
        entity.Salary = request.Salary;
        entity.Status = request.Status;
        entity.SalaryType = request.SalaryType;
        entity.ContractType = request.ContractType;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
