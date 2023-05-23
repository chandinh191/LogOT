using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.HRStaffLeaveLogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LeaveLogs.Queries;

public class GetAllLeaveLogsQuery : IRequest<List<LeaveLogDTO>> 
{ 
    
}

public class GetAllLeaveLogsQueryHandler : IRequestHandler<GetAllLeaveLogsQuery, List<LeaveLogDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllLeaveLogsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public  Task<List<LeaveLogDTO>> Handle(GetAllLeaveLogsQuery request, CancellationToken cancellationToken)
    {
        var list =  _context.LeaveLog
             .Where(e => e.IsDeleted == false)
             .ProjectTo<LeaveLogDTO>(_mapper.ConfigurationProvider)
             .ToListAsync();
        return list;
    }

}