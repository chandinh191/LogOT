using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Experiences.Commands;

public class UpdateEmployeeExpericenCommand : IRequest<ExperienceDTO>
{
    public ExperienceDTO Experience { get; set; }
    public Guid Id { get; set; }

    public UpdateEmployeeExpericenCommand(ExperienceDTO experience, Guid id)
    {
        Experience = experience;
        Id = id;
    }

    public UpdateEmployeeExpericenCommand(Guid id)
    {
        Id = id;
    }
}

public class UpdateEmployeeExperienceCommandHandler : IRequestHandler<UpdateEmployeeExpericenCommand, ExperienceDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEmployeeExperienceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExperienceDTO> Handle(UpdateEmployeeExpericenCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employee
            .Where(e => e.Id == request.Id)
            .FirstOrDefault();

        var returnExp = _context.Experience
            .Where(exp => exp.EmployeeId == request.Id)
            .Include(e => e.Employee.ApplicationUser)
            .ProjectTo<ExperienceDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        if (request.Experience != null)
        {
            var updateExp = _context.Experience
            .Where(exp => exp.EmployeeId == request.Id)
            .Include(e => e.Employee.ApplicationUser)
            .FirstOrDefault();

            updateExp.NameProject = request.Experience.NameProject;
            updateExp.TeamSize = request.Experience.TeamSize;
            updateExp.StartDate = request.Experience.StartDate;
            updateExp.EndDate = request.Experience.EndDate;
            updateExp.Description = request.Experience.Description;
            updateExp.TechStack = request.Experience.TechStack;

            _context.Experience.Update(updateExp);
            
        }
        return returnExp;
    }
}