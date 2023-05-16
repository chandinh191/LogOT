using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class Skill_Employee
{
    [Key]
    public int Id { get; set; }
    public string Level { get; set; }
    //Relationship

    //public Employee Employee {get; set;}
    public  Skill Skill { get; set; }
}
