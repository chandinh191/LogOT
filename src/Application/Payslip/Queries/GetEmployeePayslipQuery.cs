using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.OvertimeLogs;
using LogOT.Application.OvertimeLogs.Commands;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;

namespace LogOT.Application.Payslip.Queries;

public class GetEmployeePayslipQuery : IRequest<PayslipDTO>
{
    public Guid Id { get; set; }
    public GetEmployeePayslipQuery(Guid id)
    {
        Id = id;
    }
}
public class GetEmployeePayslipQueryHandler : IRequestHandler<GetEmployeePayslipQuery, PayslipDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeePayslipQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PayslipDTO> Handle(GetEmployeePayslipQuery request, CancellationToken cancellationToken)
    {
        var entity = _context.PaySlip
            .Where(o => o.Id == request.Id)
            .ProjectTo<PayslipDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
        if (entity == null)
        {
            throw new NotFoundException(nameof(PayslipDTO), request.Id);
        }
        return entity;
    }
}

