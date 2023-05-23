using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;

namespace LogOT.Application.HRStaffLeaveLogs;
public class LeaveLogDTO : BaseAuditableEntity, IMapFrom<LeaveLog>
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveHours { get; set; }
    public string Reason { get; set; }
    public LeaveLogStatus Status { get; set; }
    public bool IsDeleted { get; set; }
    public virtual Employee Employee { get; set; }
}
