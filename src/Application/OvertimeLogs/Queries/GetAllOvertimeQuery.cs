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

public class GetAllOvertimeQuery : IRequest<List<OvertimeLogDTO>> 
{ 
    
}

public class GetAllOvertimeQueryHandler : IRequestHandler<GetAllOvertimeQuery, List<OvertimeLogDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllOvertimeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OvertimeLogDTO>> Handle(GetAllOvertimeQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.OvertimeLog        
            .ProjectTo<OvertimeLogDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

}