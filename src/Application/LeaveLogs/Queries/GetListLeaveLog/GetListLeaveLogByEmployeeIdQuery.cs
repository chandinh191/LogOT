using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.EmployeeContracts.Queries;
using LogOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LeaveLogs.Queries.GetListLeaveLog;
public record GetListLeaveLogByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<LeaveLog>>;
public class GetListLeaveLogByEmployeeIdQueryHandler : IRequestHandler<GetListLeaveLogByEmployeeIdQuery, List<LeaveLog>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListLeaveLogByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveLog>> Handle(GetListLeaveLogByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var leaveLogs = await _context.LeaveLog
            .Where(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false)
            .ToListAsync(cancellationToken);

        return leaveLogs;
    }
}
