using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LogOT.Domain.IdentityModel;

namespace LogOT.Domain.Entities;
public class Employee : BaseAuditableEntity
{
    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }
    [Column("IdentityNumber")]
    [Display(Name = " IdentityNumber")]
    [StringLength(30, ErrorMessage = ("Identity Number must be less than 20 characters"))]
    [Required(ErrorMessage = "Identity Number is required")]
    public string IdentityNumber { get; set; }
    [Column("BirthDay")]
    [Display(Name = " BirthDay")]
    [Required(ErrorMessage = "employee Birthday is required")]
    public DateTime BirthDay { get; set; }
    [Column("BankAccountNumber")]
    [Display(Name = " BankAccountNumber")]
    [StringLength(19, ErrorMessage = ("Bank Account Number must be less than 19 characters"))]
    [Required(ErrorMessage = "Bank Account Number is required")]
    public string BankAccountNumber { get; set; }
    [Column("BankAccountName")]
    [Display(Name = " BankAccountName")]
    [StringLength(20, ErrorMessage = ("Bank Account Name must be less than 20 characters"))]
    [Required(ErrorMessage = "Bank Account Name is required")]
    public string BankAccountName { get; set; }
    [Column("BankName")]
    [Display(Name = " BankName")]
    [StringLength(20, ErrorMessage = ("Bank Name must be less than 20 characters"))]
    [Required(ErrorMessage = "Bank Name is required")]
    public string BankName { get; set; }

    public IList<Experience> Experiences { get; private set; }
    public IList<OvertimeLog> OvertimeLogs { get; private set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }

    public IList<EmployeeContract> EmployeeContracts { get; private set; }
    public IList<InterviewProcess> InterviewProcesses { get; private set; }
    public IList<Skill_Employee> Skill_Employees { get; private set; }


    public virtual ApplicationUser ApplicationUser { get; set; }




}
