using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LeaveLogs.Commands.UpdateLeaveLog;
public record UpdateLeaveLogCommand : IRequest
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public LeaveLogStatus Status { get; set; }
}

public class UpdateLeaveLogCommandHandler : IRequestHandler<UpdateLeaveLogCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateLeaveLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveLog
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(EmployeeContract), request.Id);
        }

        TimeSpan duration = request.EndDate - request.StartDate;
        int leaveDays = duration.Days + 1;
        int leaveHours = leaveDays * 8;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = leaveHours;
        entity.Reason = request.Reason;
        entity.Status = request.Status;
        entity.LastModified = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

