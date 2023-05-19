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
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees.Queries.GetEmployee;
public record GetEmployee : IRequest<List<EmployeeDTO>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetEmployeeWithPaginationQueryHandler : IRequestHandler<GetEmployee, List<EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetEmployeeWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDTO>> Handle(GetEmployee request, CancellationToken cancellationToken)
    {
        var list = await _context.Employee
            .Include(e => e.Experiences)
            .Where(e => e.IsDeleted == false)
            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }





}