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
using LogOT.Application.LeaveLogs;
using LogOT.Application.TodoLists.Commands.UpdateTodoList;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;

namespace LogOT.Application.HRStaffLeaveLogs.Commands;
public class UpdateNotSolvedStatusCommand : IRequest<LeaveLogDTO>
{
    public Guid Id { get; set; }
    //public OvertimeLogDTO Dto { get; set; }
    public UpdateNotSolvedStatusCommand(Guid id)
    {
        Id = id;
        //Dto=dto;
    }
}

public class UpdateNotSolvedStatusCommandHandler : IRequestHandler<UpdateNotSolvedStatusCommand, LeaveLogDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateNotSolvedStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LeaveLogDTO> Handle(UpdateNotSolvedStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.LeaveLog
            .Where(o => o.Id == request.Id)
            .ProjectTo<LeaveLogDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
        var newEntity = _context.LeaveLog
           .Where(o => o.Id == request.Id)
           .FirstOrDefault();

        //if (entity == null)
        //{
        //    throw new NotFoundException(nameof(OvertimeLog), request.Id);
        //}
        //var status = StatusOvertimeLog.Approved;
        newEntity.Status = LeaveLogStatus.notSolved;
        entity.Status = LeaveLogStatus.notSolved;
        _context.LeaveLog.Update(newEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}