using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.EmployeeContracts.Queries.GetListEmployeeContract;
public record GetListEmployeeContractQuery : IRequest<List<EmployeeContractDto>>;
public class GetListEmployeeContractQueryHandler : IRequestHandler<GetListEmployeeContractQuery, List<EmployeeContractDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListEmployeeContractQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeContractDto>> Handle(GetListEmployeeContractQuery request, CancellationToken cancellationToken)
    {
        var employeeContracts = await _context.EmployeeContract
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Created)
            .ToListAsync(cancellationToken);

        // Lấy danh sách EmployeeId từ danh sách EmployeeContract
        var employeeIds = employeeContracts.Select(x => x.EmployeeId).ToList();

        // Lấy danh sách Employee từ danh sách EmployeeId
        var employees = await _context.Employee
            .Include(e => e.ApplicationUser)
            .Where(x => employeeIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var employeeContractDtos = _mapper.Map<List<EmployeeContractDto>>(employeeContracts);

        // Map thông tin name vào từ ApplicationUser
        foreach (var contractDto in employeeContractDtos)
        {
            var employee = employees.FirstOrDefault(x => x.Id == contractDto.EmployeeId);
            if (employee != null)
            {
                contractDto.EmployeeName = employee.ApplicationUser.Fullname;
            }
        }

        return employeeContractDtos;
    }
}
