using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }

    DbSet<Company> Companies { get; }
    DbSet<CompanyContract> CompanyContracts { get; }
    DbSet<Experience> Experiences { get; }
    DbSet<Exchange> Exchanges { get; }
    DbSet<DetailTaxIncome> DetailTaxIncomes { get; }
    DbSet<PaySlip> PaySlips { get; }
    DbSet<EmployeeContract> EmployeeContracts { get; }
    DbSet<PaymentHistory> PaymentHistories { get; }
    DbSet<Holiday> Holidays { get; }
    DbSet<InterviewProcess> InterviewProcesses { get; }
    DbSet<JobDescription> JobDescriptions { get; }
    DbSet<LeaveLog> LeaveLogs { get; }
    DbSet<OvertimeLog> OvertimeLogs { get; }
    DbSet<Skill> Skills { get; }
    DbSet<Skill_Employee> Skill_Employees { get; }
    DbSet<Skill_JD> Skill_JDs { get; }
    DbSet<Employee> Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    DbSet<T> Get<T>() where T : BaseAuditableEntity;
}
