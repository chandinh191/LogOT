using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class PaymentHistory
{
    
    public string PaymentHistoryId { get; set; } = null;
    public decimal? Total { get; set; }
    public decimal? Tax { get; set; }
    public string? Note { get; set; }
    public decimal? TotalDeduction { get; set; }
    public DateTime? Date { get; set; }
    public decimal? TotalBonus { get; set; }
    public string? AcceptanceCode { get; set;}
    public string? Status { get; set; }

    //relationship
    public virtual CompanyContract Invoice { get; set; } = null!;
}
