using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Experience> Experiences { get; }

    DbSet<Exchange> Exchange { get; }
    DbSet<DetailTaxIncome> DetailTaxIncome { get; }
    DbSet<PaySlip> PaySlip { get; }
    DbSet<EmployeeContract> EmployeeContract { get; }

    DbSet<CompanyContract> CompanyContracts { get; }
    DbSet<PaymentHistory> PaymentHistory { get; }


    DbSet<Employee> Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<T> Get<T>() where T : BaseAuditableEntity;
}
