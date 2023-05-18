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

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList> TodoList => Set<TodoList>();
    public DbSet<TodoItem> TodoItem => Set<TodoItem>();

    public DbSet<Employee> Employee => Set<Employee>();
    public DbSet<Experience> Experience => Set<Experience>();
    public DbSet<Exchange> Exchange => Set<Exchange>();
    public DbSet<DetailTaxIncome> DetailTaxIncome => Set<DetailTaxIncome>();
    public DbSet<PaySlip> PaySlip => Set<PaySlip>();
    public DbSet<EmployeeContract> EmployeeContract => Set<EmployeeContract>();
    public DbSet<Company> Company => Set<Company>();
    public DbSet<CompanyContract> CompanyContract => Set<CompanyContract>();
    public DbSet<Holiday> Holiday => Set<Holiday>();
    public DbSet<InterviewProcess> InterviewProcess => Set<InterviewProcess>();
    public DbSet<JobDescription> JobDescription => Set<JobDescription>();
    public DbSet<LeaveLog> LeaveLog => Set<LeaveLog>();
    public DbSet<OvertimeLog> OvertimeLog => Set<OvertimeLog>();
    public DbSet<PaymentHistory> PaymentHistory => Set<PaymentHistory>();
    public DbSet<Skill> Skill => Set<Skill>();
    public DbSet<Skill_Employee> Skill_Employee => Set<Skill_Employee>();
    public DbSet<Skill_JD> Skill_JD => Set<Skill_JD>();

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