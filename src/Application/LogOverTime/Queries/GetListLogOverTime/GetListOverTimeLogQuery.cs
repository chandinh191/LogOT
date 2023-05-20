using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LogOverTime.Queries.GetListLogOverTime;
public record GetListOverTimeLogQuery : IRequest<List<LogOverTimeDto>>;

public class GetListOverTimeLogQueryHandler : IRequestHandler<GetListOverTimeLogQuery, List<LogOverTimeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListOverTimeLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<LogOverTimeDto>> Handle(GetListOverTimeLogQuery request, CancellationToken cancellationToken)
    {
        var overtimeLog = await _context.OvertimeLog
            .Where(x => x.IsDeleted == false)
            .OrderBy(x => x.Created)
            .ToListAsync(cancellationToken);

       
        var overtimelogid = overtimeLog.Select(x => x.EmployeeId).ToList();

       
        var overtimelog = await _context.OvertimeLog
            .Include(e => e.Employee.ApplicationUser)
            .Where(x => overtimelogid.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var overtimeLogDtos = _mapper.Map<List<LogOverTimeDto>>(overtimeLog);

      
        //foreach (var contractDto in overtimeLogDtos)
        //{
            //var overtimelog = overtimelogid.FirstOrDefault(x => x. == contractDto.EmployeeId);
            //if (employee != null)
            //{
                //contractDto.EmployeeId = employee.ApplicationUser.Fullname;
            //}
        //}

        return overtimeLogDtos;
    }

}
