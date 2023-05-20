using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees_Skill.Queries;

public class GetEmployeeSkillQuery : IRequest<List<Skill_EmployeeDTO>>
{
    public Guid Id { get; set; }

    public GetEmployeeSkillQuery(Guid id)
    {
        Id = id;
    }
}

public class GetEmployeeSkillQueryHandler : IRequestHandler<GetEmployeeSkillQuery, List<Skill_EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeSkillQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Skill_EmployeeDTO>> Handle(GetEmployeeSkillQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Skill_Employee
            .Include(k => k.Skill)
            .Include(e => e.Employee.ApplicationUser)
            .Where(s => s.EmployeeId.Equals(request.Id))
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }
}