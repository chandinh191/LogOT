using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.Employees_Skill;
using LogOT.Application.LogOverTime.Queries;
using LogOT.Application.TodoLists.Commands.UpdateTodoList;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.OvertimeLogs.Commands;
public record UpdateApprovedStatusCommand(Guid Id) : IRequest<OvertimeLogDTO>;


public class UpdateApprovedStatusCommandHandler : IRequestHandler<UpdateApprovedStatusCommand, OvertimeLogDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateApprovedStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OvertimeLogDTO> Handle(UpdateApprovedStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLog
            .Where(x => x.Id == request.Id && x.IsDeleted == false)
            .FirstOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }
        var status = StatusOvertimeLog.Approved;      
        entity.Status = status.ToString();
        _context.OvertimeLog.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);


        var newEntity = _mapper.Map<OvertimeLogDTO>(entity);
        return newEntity;
    }
}