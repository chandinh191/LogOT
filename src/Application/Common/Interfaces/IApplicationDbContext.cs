﻿using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoList { get; }
    DbSet<TodoItem> TodoItem { get; }

    DbSet<Company> Company { get; }
    DbSet<CompanyContract> CompanyContract { get; }
    DbSet<Experience> Experience { get; }
    DbSet<Exchange> Exchange { get; }
    DbSet<DetailTaxIncome> DetailTaxIncome { get; }
    DbSet<PaySlip> PaySlip { get; }
    DbSet<EmployeeContract> EmployeeContract { get; }
    DbSet<PaymentHistory> PaymentHistory { get; }
    DbSet<Holiday> Holiday { get; }
    DbSet<InterviewProcess> InterviewProcess { get; }
    DbSet<JobDescription> JobDescription { get; }
    DbSet<LeaveLog> LeaveLog { get; }
    DbSet<OvertimeLog> OvertimeLog { get; }
    DbSet<Skill> Skill { get; }
    DbSet<Skill_Employee> Skill_Employee { get; }
    DbSet<Skill_JD> Skill_JD { get; }
    DbSet<Employee> Employee { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<T> Get<T>() where T : BaseAuditableEntity;
}