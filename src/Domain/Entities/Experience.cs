﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LogOT.Domain.Entities;

public class Experience : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    public string NameProject { get; set; }
    public int TeamSize { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Description { get; set; }
    public string TechStack { get; set; }

    public virtual Employee Employee { get; set; }

    public string Status { get; set; }
    public bool IsDeleted { get; set; }
}