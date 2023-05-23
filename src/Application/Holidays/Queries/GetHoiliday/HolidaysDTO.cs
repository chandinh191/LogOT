using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Entities;

namespace LogOT.Application.Holidays.Queries.GetHoiliday;
public class HolidaysDTO : IMapFrom<Holiday> 
{
    public Guid Id { get; set; }
    public string DateName { get; set; }
    public DateTime Day { get; set; }
    public decimal HourlyPay { get; set; }

    public bool IsDeleted { get; set; }
}
