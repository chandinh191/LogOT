﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LogOT.Domain.Entities;

public class Skill_Employee : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [ForeignKey("Skill")]
    public Guid SkillId { get; set; }

    public string Level { get; set; }

    //Relationship
    public virtual Employee? Employee { get; set; }

    public virtual Skill? Skill { get; set; }
}