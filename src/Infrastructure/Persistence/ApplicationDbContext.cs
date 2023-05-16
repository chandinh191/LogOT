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


    public DbSet<Exchange> Exchange => Set<Exchange>();
    public DbSet<DetailTaxIncome> DetailTaxIncome => Set<DetailTaxIncome>();
    public DbSet<PaySlip> PaySlip => Set<PaySlip>();
    public DbSet<EmployeeContract> EmployeeContract => Set<EmployeeContract>();


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
}
