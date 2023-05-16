using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class CompanyContract
{
    
    public string CompanyContractId { get; set; } = null;
    public decimal? Price { get; set; }
    public int? DayOff { get; set; }
    public string? ContactCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? File { get; set; }

    //realtionship
    public virtual ICollection<PaymentHistory> PaymentHistories { get; } 
                        = new List<PaymentHistory>();
}
