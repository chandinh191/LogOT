using System.Reflection;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Infrastructure.Identity;
using LogOT.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using LogOT.Domain.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LogOT.Domain.Common;

namespace LogOT.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Exchange> Exchanges => Set<Exchange>();
    public DbSet<DetailTaxIncome> DetailTaxIncomes => Set<DetailTaxIncome>();
    public DbSet<PaySlip> PaySlips => Set<PaySlip>();
    public DbSet<EmployeeContract> EmployeeContracts => Set<EmployeeContract>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyContract> CompanyContracts => Set<CompanyContract>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<InterviewProcess> Interviews => Set<InterviewProcess>();
    public DbSet<JobDescription> JobDescription => Set<JobDescription>();
    public DbSet<LeaveLog> LeaveLog => Set<LeaveLog>();
    public DbSet<OvertimeLog> OvertimeLogs=> Set<OvertimeLog>();
    public DbSet<PaymentHistory> PaymentHistory => Set<PaymentHistory>();
    public DbSet<Skill> Skill => Set<Skill>();
    public DbSet<Skill_Employee> Skill_Employees=> Set<Skill_Employee>();
    public DbSet<Skill_JD> Skill_JDs=> Set<Skill_JD>();

    DbSet<TodoList> IApplicationDbContext.TodoLists => throw new NotImplementedException();

    DbSet<TodoItem> IApplicationDbContext.TodoItems => throw new NotImplementedException();

    DbSet<Company> IApplicationDbContext.Companies => throw new NotImplementedException();

    DbSet<CompanyContract> IApplicationDbContext.CompanyContracts => throw new NotImplementedException();

    DbSet<Experience> IApplicationDbContext.Experiences => throw new NotImplementedException();

    DbSet<Exchange> IApplicationDbContext.Exchanges => throw new NotImplementedException();

    DbSet<DetailTaxIncome> IApplicationDbContext.DetailTaxIncomes => throw new NotImplementedException();

    DbSet<PaySlip> IApplicationDbContext.PaySlips => throw new NotImplementedException();

    DbSet<EmployeeContract> IApplicationDbContext.EmployeeContracts => throw new NotImplementedException();

    DbSet<PaymentHistory> IApplicationDbContext.PaymentHistories => throw new NotImplementedException();

    DbSet<Holiday> IApplicationDbContext.Holidays => throw new NotImplementedException();

    DbSet<InterviewProcess> IApplicationDbContext.InterviewProcesses => throw new NotImplementedException();

    DbSet<JobDescription> IApplicationDbContext.JobDescriptions => throw new NotImplementedException();

    DbSet<LeaveLog> IApplicationDbContext.LeaveLogs => throw new NotImplementedException();

    DbSet<OvertimeLog> IApplicationDbContext.OvertimeLogs => throw new NotImplementedException();

    DbSet<Skill> IApplicationDbContext.Skills => throw new NotImplementedException();

    DbSet<Skill_Employee> IApplicationDbContext.Skill_Employees => throw new NotImplementedException();

    DbSet<Skill_JD> IApplicationDbContext.Skill_JDs => throw new NotImplementedException();

    DbSet<Employee> IApplicationDbContext.Employees => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<T> Get<T>() where T : BaseAuditableEntity => Set<T>();

    Task<int> IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    DbSet<T> IApplicationDbContext.Get<T>()
    {
        throw new NotImplementedException();
    }
}
