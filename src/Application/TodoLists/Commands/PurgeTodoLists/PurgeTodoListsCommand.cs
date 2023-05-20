﻿using LogOT.Application.Common.Interfaces;
using LogOT.Application.Common.Security;
using MediatR;

namespace LogOT.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoList.RemoveRange(_context.TodoList);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
