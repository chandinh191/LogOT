using System.ComponentModel.DataAnnotations;

namespace LogOT.Domain.Entities;

public class Holiday
{
    [Key]
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }

    //Relationship
    public virtual Company Company { get; set; }
}