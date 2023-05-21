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

namespace LogOT.Application.Payslip.Queries;

public class GetAllPayslipQuery : IRequest<List<PayslipDTO>>
{

}

public class GetAllPayslipQueryHandler : IRequestHandler<GetAllPayslipQuery, List<PayslipDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPayslipQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PayslipDTO>> Handle(GetAllPayslipQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.PaySlip
            .ProjectTo<PayslipDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

}