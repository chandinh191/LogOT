using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Entities;

namespace LogOT.Application.Employees.Queries.GetEmployee;
public class EmployeeDTO : IMapFrom<Employee>
{
    [ForeignKey("ApplicationUser")]
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public string ApplicationUserId { get; set; }

    public string IdentityNumber { get; set; }
    public DateTime BirthDay { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string BankName { get; set; }
}
