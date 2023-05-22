using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees.Queries;

public class GetAllEmployeeWithPaginationQuery : IRequest<List<EmployeeDTO>>
{
}

public class GetAllEmployeeWithPaginationQueryHandler : IRequestHandler<GetAllEmployeeWithPaginationQuery, List<EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEmployeeWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDTO>> Handle(GetAllEmployeeWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var listtest = _context.Employee.ToList();
        var list = await _context.Employee
            .Include(e => e.Experiences)
            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}