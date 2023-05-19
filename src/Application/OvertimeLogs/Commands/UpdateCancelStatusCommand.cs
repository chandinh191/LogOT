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
using LogOT.Application.TodoLists.Commands.UpdateTodoList;
using LogOT.Domain.Entities;
using MediatR;

namespace LogOT.Application.OvertimeLogs.Commands;
public class UpdateCancelStatusCommand : IRequest<OvertimeLogDTO>
{
    public Guid Id { get; set; }
    //public OvertimeLogDTO Dto { get; set; }
    public UpdateCancelStatusCommand(Guid id)
    {
        Id = id;
        //Dto=dto;
    }
}

public class UpdateCancelStatusCommandHandler : IRequestHandler<UpdateCancelStatusCommand, OvertimeLogDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCancelStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OvertimeLogDTO> Handle(UpdateCancelStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.OvertimeLog
            .Where(o => o.Id == request.Id)
            .ProjectTo<OvertimeLogDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
        var newEntity = _context.OvertimeLog
           .Where(o => o.Id == request.Id)
           .FirstOrDefault();
        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }

        newEntity.Status = "Cancel";
        entity.Status = "Cancel";
        _context.OvertimeLog.Update(newEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}