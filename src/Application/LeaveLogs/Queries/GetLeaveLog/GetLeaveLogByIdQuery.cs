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

namespace LogOT.Application.LeaveLogs.Queries.GetLeaveLog;
public record GetLeaveLogByIdQuery(Guid Id) : IRequest<LeaveLog>;
public class GetLeaveLogByIdQueryHandler : IRequestHandler<GetLeaveLogByIdQuery, LeaveLog>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLeaveLogByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LeaveLog> Handle(GetLeaveLogByIdQuery request, CancellationToken cancellationToken)
    {
        var leaveLog = await _context.LeaveLog
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return leaveLog;
    }
}

