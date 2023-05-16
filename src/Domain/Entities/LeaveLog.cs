using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class LeaveLog
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveHours { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
}
