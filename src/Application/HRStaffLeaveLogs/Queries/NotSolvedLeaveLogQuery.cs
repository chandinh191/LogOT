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

public class NotSolvedLeaveLogQuery : IRequest<List<LeaveLogDTO>>
{
    public Guid Id { get; set; }
}

public class NotSolvedLeaveLogQueryHandler : IRequestHandler<NotSolvedLeaveLogQuery, List<LeaveLogDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public NotSolvedLeaveLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveLogDTO>> Handle(NotSolvedLeaveLogQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.LeaveLog
            .Where(o => o.Status.Equals(LeaveLogStatus.notSolved))
            .ProjectTo<LeaveLogDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

}