﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.HRStaffLeaveLogs;
using LogOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.HRStaffLeaveLogs.Queries;

public class PendingOvertimeLogQuery : IRequest<List<LeaveLogDTO>>
{
    public Guid Id { get; set; }
}

public class PendingOvertimeLogQueryHandler : IRequestHandler<PendingOvertimeLogQuery, List<LeaveLogDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PendingOvertimeLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveLogDTO>> Handle(PendingOvertimeLogQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.LeaveLog
            .Where(o => o.Status.Equals(LeaveLogStatus.pending))
            .ProjectTo<LeaveLogDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

}