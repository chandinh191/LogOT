using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class EmployeeContract
{
    public string EmployeeContractId { get; set; } = null!;
    public string EmployeeId { get; set; } = null!;
    public string? file { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? BasicSalary { get; set; }
    public string? Status { get; set; }
    public double? PercentDeduction { get; set; }
    public string? SalaryType { get; set; }
    public string? ContractType { get; set; }
}
