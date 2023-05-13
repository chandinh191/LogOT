using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Exchange> Exchange { get; }
    DbSet<DetailTaxIncome> DetailTaxIncome { get; }
    DbSet<PaySlip> PaySlip { get; }
    DbSet<EmployeeContract> EmployeeContract { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
