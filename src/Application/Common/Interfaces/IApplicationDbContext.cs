using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<CompanyContract> CompanyContracts { get; }
    DbSet<PaymentHistory> PaymentHistory { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
