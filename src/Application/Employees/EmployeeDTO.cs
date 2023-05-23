using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Application.Experiences;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;

namespace LogOT.Application.Employees;

public class EmployeeDTO : BaseAuditableEntity, IMapFrom<Employee>
{
    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string BankName { get; set; }
   
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string PhoneNumber { get; set; }
    public IList<ExperienceDTO> Experiences { get; private set; }
    public IList<OvertimeLog> OvertimeLogs { get; private set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }

    public IList<EmployeeContract> EmployeeContracts { get; private set; }
    public IList<InterviewProcess> InterviewProcesses { get; private set; }
    public IList<Skill_Employee> Skill_Employees { get; private set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
}