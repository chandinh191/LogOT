using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Experiences.Commands;

public class DeleteEmployeeExperienceCommand : IRequest<ExperienceDTO>
{
    public Guid Id { get; set; }
    public ExperienceDTO Experience { get; set; }

    public DeleteEmployeeExperienceCommand(Guid id, ExperienceDTO experience)
    {
        Id = id;
        Experience = experience;
    }
    public DeleteEmployeeExperienceCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteEmployeeExperienceCommandHandler : IRequestHandler<DeleteEmployeeExperienceCommand, ExperienceDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteEmployeeExperienceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExperienceDTO> Handle(DeleteEmployeeExperienceCommand request, CancellationToken cancellationToken)
    {
        var experience = _context.Experience
            .Where(exp => exp.EmployeeId == request.Id)
            .FirstOrDefault();

        var returnExp = _context.Experience
            .Where(exp => exp.EmployeeId == request.Id)
            .Include(e => e.Employee.ApplicationUser)
            .ProjectTo<ExperienceDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        if (request.Experience != null)
        {
            experience.IsDeleted = true;
            _context.Experience.Update(experience);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return returnExp;
    }
}