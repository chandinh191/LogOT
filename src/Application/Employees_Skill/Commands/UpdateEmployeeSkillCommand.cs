using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees_Skill.Commands;

public class UpdateEmployeeSkillCommand : IRequest<Skill_EmployeeDTO>
{
    public Guid Id { get; set; }
    public Skill_EmployeeDTO Skill_EmployeeDTO { get; set; }
    public UpdateEmployeeSkillCommand(Guid id, Skill_EmployeeDTO skill_EmployeeDTO)
    {
        Id = id;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }

    public UpdateEmployeeSkillCommand(Guid id)
    {
        Id = id;
    }
}

public class UpdateEmployeeSkillCommandHandler : IRequestHandler<UpdateEmployeeSkillCommand, Skill_EmployeeDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEmployeeSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Skill_EmployeeDTO> Handle(UpdateEmployeeSkillCommand request, CancellationToken cancellationToken)
    {
        var objReturn = _context.Skill_Employee
            .Include(e => e.Employee.ApplicationUser)
            .Include(e => e.Skill)
            .Where(s => s.EmployeeId == request.Id)
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        var objUpdate = await _context.Skill_Employee
            .Include(e => e.Employee)
            .Where(s => s.EmployeeId.Equals(request.Id))
            .FirstOrDefaultAsync();
        if (objUpdate != null && request.Skill_EmployeeDTO != null)  
        {
            objUpdate.Skill.SkillName = request.Skill_EmployeeDTO.Skill.SkillName;
            objUpdate.Skill.Skill_Description = request.Skill_EmployeeDTO.Skill.Skill_Description;
            objUpdate.Level = request.Skill_EmployeeDTO.Level;

             _context.Skill_Employee.Update(objUpdate);
        }
        return objReturn;
    }
}