﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;

namespace LogOT.Application.Employees.Queries.GetEmployee;
public class EmployeeDTO : IMapFrom<Employee>
{
    [ForeignKey("ApplicationUser")]
    public Guid Id { get; set; }
    public string ApplicationUserId { get; set; }

    public string IdentityNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string BankName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Created { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }

    //public IList<ExperienceDTO> Experiences { get; private set; }
    public IList<OvertimeLog> OvertimeLogs { get; private set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }

    public IList<EmployeeContract> EmployeeContracts { get; private set; }
    public IList<InterviewProcess> InterviewProcesses { get; private set; }
    public IList<Skill_Employee> Skill_Employees { get; private set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
}
