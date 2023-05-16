using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Experience> Experiences { get; }

    DbSet<Exchange> Exchanges { get; }
    DbSet<DetailTaxIncome> DetailTaxIncomes { get; }
    DbSet<PaySlip> PaySlips { get; }
    DbSet<EmployeeContract> EmployeeContracts { get; }

    DbSet<CompanyContract> CompanyContracts { get; }
    DbSet<PaymentHistory> PaymentHistorys { get; }


    DbSet<Employee> Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<T> Get<T>() where T : BaseAuditableEntity;
}
