using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.LeaveLogs.Commands.CreateLeaveLog;

public record CreateLeaveLogCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
}

public class CreateLeaveLogCommandHandler : IRequestHandler<CreateLeaveLogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateLeaveLogCommand request, CancellationToken cancellationToken)
    {
        
        TimeSpan duration = request.EndDate - request.StartDate;
        int leaveDays = duration.Days + 1;
        int leaveHours = leaveDays * 8;
        var employee = await _context.Employee
            .Include(e => e.ApplicationUser)
            .Where(x => x.Id == request.EmployeeId)
            .FirstOrDefaultAsync(cancellationToken);
        var entity = new LeaveLog();
        entity.EmployeeId = request.EmployeeId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = leaveHours;
        entity.Reason = request.Reason;
        entity.Status = LeaveLogStatus.pending;
        entity.CreatedBy = employee.ApplicationUser.Fullname;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = employee.ApplicationUser.Fullname;

        _context.LeaveLog.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
