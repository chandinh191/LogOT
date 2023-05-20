using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Interfaces;
using LogOT.Application.TodoLists.Commands.CreateTodoList;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using MediatR;

namespace LogOT.Application.LogOverTime.Commands.CreateLogOverTime;
public class CreateLogOvertimeCommand : IRequest<Guid>
{
    

    public Guid EmployeeId { get; set; }

    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string? Status { get; set; }
    public bool IsDeleted { get; set; }


}
public class CreateLogOvertimeCommandHandler : IRequestHandler<CreateLogOvertimeCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateLogOvertimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> Handle(CreateLogOvertimeCommand request, CancellationToken cancellationToken)
    {
        var entity = new OvertimeLog();

        
        entity.EmployeeId = request.EmployeeId;
        entity.Date  = request.Date;
        entity.Hours  = request.Hours;
        entity.Status  = request.Status;
        entity.IsDeleted = request.IsDeleted;   




        _context.OvertimeLog.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

