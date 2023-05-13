using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string AccountEmail { get; set; }
    public string Phone { get; set; }
    public string HREmail { get; set; }

    // Relationship
    public virtual ICollection<Holiday> Holidays { get; set; }
    public virtual ICollection<JobDescription> JobDescriptions { get; set; }
}
