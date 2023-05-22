using AutoMapper;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Experiences.Commands;

public class AddEmployeeExperienceCommand : IRequest<ExperienceDTO>
{
    public ExperienceDTO Experience { get; set; }
    public Guid Id { get; set; }

    public AddEmployeeExperienceCommand(ExperienceDTO experience, Guid id)
    {
        Experience = experience;
        Id = id;
    }

    public AddEmployeeExperienceCommand(Guid id)
    {
        Id = id;
    }
}

public class AddEmployeeExperienceCommandHandler : IRequestHandler<AddEmployeeExperienceCommand, ExperienceDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddEmployeeExperienceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExperienceDTO> Handle(AddEmployeeExperienceCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employee
            .Include(e => e.ApplicationUser)
            .Where(e => e.Id == request.Id)
            .FirstOrDefault();

        var returnExp = new ExperienceDTO
        {
            EmployeeId = employee.Id,
            Employee = new Employee
            {
                ApplicationUser = new ApplicationUser
                {
                    Fullname = employee.ApplicationUser.Fullname
                }
            }
        };

        if (employee != null && request.Experience != null)
        {
            var experience = new Experience
            {
                Id = Guid.NewGuid(),
                NameProject = request.Experience.NameProject,
                TeamSize = request.Experience.TeamSize,
                StartDate = request.Experience.StartDate,
                EndDate = request.Experience.EndDate,
                Description = request.Experience.Description,
                TechStack = request.Experience.TechStack,
                Employee = employee,
                Status = false,
                CreatedBy = employee.ApplicationUser.UserName,
                LastModifiedBy = employee.ApplicationUser.UserName
            };

            await _context.Experience.AddAsync(experience);
            //await _context.SaveChangesAsync(cancellationToken);
        }

        return returnExp;
    }
}