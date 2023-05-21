using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LogOverTime.Queries.GetLogOverTime;
public record GetOverLogByIdQuery(Guid Id) : IRequest<LogOverTimeDto>;
public class GetOverLogByIdQueryHandler : IRequestHandler<GetOverLogByIdQuery, LogOverTimeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetOverLogByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<LogOverTimeDto> Handle(GetOverLogByIdQuery request, CancellationToken cancellationToken)
    {
        var overtimelog = await _context.OvertimeLog
            .Where(x => x.Id == request.Id && x.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        if (overtimelog == null)
        {
                
            return null;
        }
        var employee = await _context.Employee
            .Include(e => e.ApplicationUser)
            .Where(x => x.Id == overtimelog.EmployeeId)
            .FirstOrDefaultAsync(cancellationToken);

        var logOverTimeDto = _mapper.Map<LogOverTimeDto>(overtimelog);



        logOverTimeDto.EmployeeId = employee.Id;

        return logOverTimeDto;
    }
}
