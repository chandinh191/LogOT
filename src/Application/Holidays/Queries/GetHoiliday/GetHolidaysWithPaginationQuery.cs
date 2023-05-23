using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Holidays.Queries.GetHoiliday;
public record GetHolidaysWithPaginationQuery : IRequest<List<HolidaysDTO>>
{
}

public class GetHolidaysWithPaginationQueryHandler :
        IRequestHandler<GetHolidaysWithPaginationQuery, List<HolidaysDTO>>
{
        private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetHolidaysWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HolidaysDTO>>
        Handle(GetHolidaysWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Holiday
           .ProjectTo<HolidaysDTO>(_mapper.ConfigurationProvider)
           .ToListAsync();
        return list;
    }
}
