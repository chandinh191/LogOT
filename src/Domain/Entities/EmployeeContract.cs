using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class EmployeeContract : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public string? File { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? BasicSalary { get; set; }
    public string? Status { get; set; }
    public double? PercentDeduction { get; set; }
    public string? SalaryType { get; set; }
    public string? ContractType { get; set; }

    public virtual Employee Employee { get; set; }

    public IList<PaySlip> PaySlips { get; private set; }
}
