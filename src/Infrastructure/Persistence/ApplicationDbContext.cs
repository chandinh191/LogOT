using System.Reflection;
using System.Reflection.Emit;
using Duende.IdentityServer.EntityFramework.Options;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using LogOT.Domain.IdentityModel;
using LogOT.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

        //Seeding database
        builder.Entity<ApplicationUser>()
            .HasData(
            new ApplicationUser
            {
                Id = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                Fullname = "Lewis",
                Address = "TEST",
                Image = "TESTIMAGE",
                UserName = "test",
                NormalizedUserName = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "098f6bcd4621d373cade4e832627b4f6",
                SecurityStamp = "test",
                ConcurrencyStamp = "test",
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = DateTimeOffset.Parse("9/9/9999 12:00:00 AM +07:00"),
                LockoutEnabled = false,
                AccessFailedCount = 0
            }
        );

        builder.Entity<Employee>()
            .HasData(
            new Employee
            {
                Id = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                ApplicationUserId = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                IdentityNumber = "SE1615",
                BirthDay = DateTime.Parse("9/9/9999"),
                BankAccountNumber = "123456789",
                BankAccountName = "LUONG THE DAN",
                BankName = "TECHCOMBANK",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );
        builder.Entity<Employee>()
            .HasData(
            new Employee
            {
                Id = Guid.Parse("bc69dc8e-f88d-46c2-a861-c9d5ac894142"),
                ApplicationUserId = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                IdentityNumber = "SE1610",
                BirthDay = DateTime.Parse("9/9/2002"),
                BankAccountNumber = "123456789",
                BankAccountName = "TRAN CONG VINH",
                BankName = "TECHCOMBANK",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );


        builder.Entity<Experience>()
                .HasData(
                    new Experience
                    {
                        Id = Guid.Parse("850df2d9-f8dc-444a-b1dc-ca773c0a2d0d"),
                        EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                        NameProject = "TestProject",
                        TeamSize = 4,
                        StartDate = DateTime.Parse("9/9/9999"),
                        EndDate = DateTime.Parse("9/9/9999"),
                        Description = "Normal",
                        TechStack = "MSSQL, .NET 7, MVC",
                        Status = "true",
                        IsDeleted = false,
                        Created = DateTime.Parse("9/9/9999"),
                        CreatedBy = "test",
                        LastModified = DateTime.Parse("9/9/9999"),
                        LastModifiedBy = "test"
                    }
                );

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


    //DbSet<T> IApplicationDbContext.Get<T>()
    //{
    //    throw new NotImplementedException();
    //}
}