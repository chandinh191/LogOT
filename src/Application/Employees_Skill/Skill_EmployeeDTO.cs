using System.ComponentModel.DataAnnotations.Schema;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;

namespace LogOT.Application.Employees_Skill;

public class Skill_EmployeeDTO : BaseAuditableEntity, IMapFrom<Skill_Employee>
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