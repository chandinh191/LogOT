using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.Common.Mappings;
using LogOT.Application.Common.Models;
using MediatR;

namespace LogOT.Application.Employees.Queries.GetEmployee;
public record GetEmployee : IRequest<PaginatedList<EmployeeDTO>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetEmployeeWithPaginationQueryHandler : IRequestHandler<GetEmployee, PaginatedList<EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetEmployeeWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<EmployeeDTO>> Handle(GetEmployee request, CancellationToken cancellationToken)
    {
        return await _context.Employee
            //.OrderBy(x => x.Name)
            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}