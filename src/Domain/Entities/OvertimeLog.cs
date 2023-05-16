using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class OvertimeLog : BaseAuditableEntity
{
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
}
