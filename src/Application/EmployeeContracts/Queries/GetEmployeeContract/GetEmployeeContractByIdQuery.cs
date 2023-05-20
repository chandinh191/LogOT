using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.EmployeeContracts.Queries.GetEmployeeContract;
public record GetEmployeeContractByIdQuery(Guid Id) : IRequest<EmployeeContractDto>;
public class GetEmployeeContractByIdQueryHandler : IRequestHandler<GetEmployeeContractByIdQuery, EmployeeContractDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeContractByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeContractDto> Handle(GetEmployeeContractByIdQuery request, CancellationToken cancellationToken)
    {
        var employeeContract = await _context.EmployeeContract
            .Where(x => x.Id == request.Id && x.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        if (employeeContract == null)
        {
            // Xử lý khi không tìm thấy hợp đồng lao động
            return null; // hoặc ném một ngoại lệ thích hợp
        }
        var employee = await _context.Employee
            .Include(e => e.ApplicationUser)
            .Where(x => x.Id == employeeContract.EmployeeId)
            .FirstOrDefaultAsync(cancellationToken);

        var employeeContractDto = _mapper.Map<EmployeeContractDto>(employeeContract);

        // Map thông tin name vào từ ApplicationUser

        employeeContractDto.EmployeeName = employee.ApplicationUser.Fullname;

        return employeeContractDto;
    }
}
