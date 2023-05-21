using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees_Skill.Commands;
public class DeleteEmployeeSkillCommand : IRequest<Skill_EmployeeDTO>
{
    public Guid Id { get; set; }
    public Skill_EmployeeDTO Skill_EmployeeDTO { get; set; }
    public DeleteEmployeeSkillCommand(Guid id, Skill_EmployeeDTO skill_EmployeeDTO)
    {
        Id = id;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }

    public DeleteEmployeeSkillCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteEmployeeSkillCommandHandler : IRequestHandler<DeleteEmployeeSkillCommand, Skill_EmployeeDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public DeleteEmployeeSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Skill_EmployeeDTO> Handle(DeleteEmployeeSkillCommand request, CancellationToken cancellationToken)
    {
        var returnSkill = await _context.Skill_Employee
            .Where(s => s.EmployeeId == request.Id)
            .Include(e => e.Employee.ApplicationUser)
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        var deleteSkill = await _context.Skill_Employee
            .Include(s => s.Skill)
            .Where(s => s.EmployeeId == request.Id)
            .FirstOrDefaultAsync();

        if (deleteSkill != null && request.Skill_EmployeeDTO != null)
        {
            deleteSkill.Skill.IsDeleted = true;
            _context.Skill_Employee.Update(deleteSkill);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return returnSkill;
    }
}
