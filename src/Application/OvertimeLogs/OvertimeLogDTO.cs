﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;

namespace LogOT.Application.OvertimeLogs;
public class OvertimeLogDTO : BaseAuditableEntity, IMapFrom<OvertimeLog>
{
    //public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
}
