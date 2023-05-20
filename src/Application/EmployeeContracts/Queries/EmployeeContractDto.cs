using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogOT.Application.Common.Mappings;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;

namespace LogOT.Application.EmployeeContracts.Queries;
public class EmployeeContractDto : IMapFrom<EmployeeContract>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public string? File { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? Salary { get; set; }
    public EmployeeContractStatus Status { get; set; }
    public SalaryType SalaryType { get; set; }
    public ContractType ContractType { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeContract, EmployeeContractDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.ApplicationUser.Fullname));
    }
}

