using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Common;
using LogOT.Domain.Entities;

namespace LogOT.Application.Experiences;

public class ExperienceDTO : BaseAuditableEntity, IMapFrom<Experience>
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }

    [RegularExpression(@"^[A-Z][^0-9].*$", ErrorMessage = "Project name must be uppercase at first letter and not start with a number!")]
    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters")]
    public string NameProject { get; set; }

    [Range(1, 10, ErrorMessage = "Team size must be greater than 0 and must lower than 10!")]
    public int TeamSize { get; set; }

    [StartDate(ErrorMessage = "Start date must be as least from today!")]
    public DateTime StartDate { get; set; }

    [EndDate(ErrorMessage = "End date must be after start date!")]
    public DateTime EndDate { get; set; }

    [RegularExpression(@"^.{10,255}$", ErrorMessage = "Description must be between 10 and 255 characters")]
    public string Description { get; set; }


    [StringLength(100, ErrorMessage = "Project name must be less than 100 characters")]
    public string TechStack { get; set; }

    public virtual Employee? Employee { get; set; }

    public bool Status { get; set; }
    //public bool IsDeleted { get; set; }
}