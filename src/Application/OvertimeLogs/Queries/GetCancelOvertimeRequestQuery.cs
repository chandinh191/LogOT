using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.OvertimeLogs.Queries;

public class GetCancelOvertimeRequestQuery : IRequest<List<OvertimeLogDTO>>
{
    public Guid Id { get; set; }
}

public class GetCancelOvertimeRequestQueryHandler : IRequestHandler<GetCancelOvertimeRequestQuery, List<OvertimeLogDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCancelOvertimeRequestQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OvertimeLogDTO>> Handle(GetCancelOvertimeRequestQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.OvertimeLog
            .Where(o => o.Status.Equals("Cancel"))
            .ProjectTo<OvertimeLogDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

}