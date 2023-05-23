using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;

namespace LogOT.Application.OvertimeLogs;
public class OvertimeLogDTO : IMapFrom<OvertimeLog>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Employee Employee { get; set; }
}
