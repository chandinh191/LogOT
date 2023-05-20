namespace LogOT.Domain.Entities;

public class Skill : BaseAuditableEntity
{
    public string SkillName { get; set; }
    public string Skill_Description { get; set; }

    // Relationship
    public ICollection<Skill_Employee> Skill_Employees { get; set; }

    public ICollection<Skill_JD> Skill_JDs { get; set; }
}