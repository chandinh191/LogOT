using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Entities;

namespace LogOT.Application.LogOverTime.Queries;
public class LogOverTimeDto : IMapFrom<OvertimeLog >
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public int Hours { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OvertimeLog, LogOverTimeDto>()
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.ApplicationUser.Fullname));
    }
}
