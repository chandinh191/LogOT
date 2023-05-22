﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees_Skill.Commands;

public class AddEmployeeSkillCommand : IRequest<Skill_EmployeeDTO>
{
    public Guid Id { get; set; }
    public Skill_EmployeeDTO Skill_EmployeeDTO { get; set; }

    public AddEmployeeSkillCommand(Guid id, Skill_EmployeeDTO skill_EmployeeDTO)
    {
        Id = id;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }

    public AddEmployeeSkillCommand(Guid id)
    {
        Id = id;
    }
}

public class AddEmployeeSkillCommandHandler : IRequestHandler<AddEmployeeSkillCommand, Skill_EmployeeDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEmployeeSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Skill_EmployeeDTO> Handle(AddEmployeeSkillCommand request, CancellationToken cancellationToken)
    {
        var EmpSkill = _context.Skill_Employee
            .Include(e => e.Employee.ApplicationUser)
            .Where(e => e.EmployeeId == request.Id)
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        var returnEmpSkill = new Skill_EmployeeDTO
        {
            EmployeeId = EmpSkill.EmployeeId,
            Employee = new Employee { ApplicationUser = new ApplicationUser { UserName = EmpSkill.Employee.ApplicationUser.UserName } },
        };

        if (request.Skill_EmployeeDTO != null)
        {
            var newSkill = new Skill
            {
                Id = Guid.NewGuid(),
                SkillName = request.Skill_EmployeeDTO.Skill.SkillName,
                Skill_Description = request.Skill_EmployeeDTO.Skill.Skill_Description,
                CreatedBy = EmpSkill.CreatedBy
            };

            var newEmpSkill = new Skill_Employee
            {
                Id = Guid.NewGuid(),
                EmployeeId = request.Id,
                Skill = newSkill,
                Level = request.Skill_EmployeeDTO.Level,
                CreatedBy = EmpSkill.CreatedBy
            };

            await _context.Skill_Employee.AddAsync(newEmpSkill);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return returnEmpSkill;
    }
}