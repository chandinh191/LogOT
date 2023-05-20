using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Application.Salaries.Commands;
public class SalaryDto
{
    public double Gross { get; set; }
    public double BHXH_Emp { get; set; }
    public double BHYT_Emp { get; set; }
    public double BHTN_Emp { get; set; }
    public double TNTT { get; set; }
    public double Dependent_Deduction { get; set; }
    public double TNCT { get; set; }
    public double TTNCN { get; set; }
    public double Net { get; set; }
    public List<double> TTNCN_Detail { get; set; }
    public double BHXH_Comp { get; set; }
    public double BHYT_Comp { get; set; }
    public double BHTN_Comp { get; set; }
    public double Total_Cmp_Salary { get; set; }
}
