using AutoMapper;
using AutoMapper.QueryableExtensions;
using LogOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Experiences.Queries;

public class GetAllExperienceQuery : IRequest<List<ExperienceDTO>>
{
    //public EmployeeDTO GetEmployee { get; set; }
    public Guid Id { get; set; }

    public GetAllExperienceQuery(Guid id)
    {
        Id = id;
    }
}

public class GetAllExperienceQueryHandler : IRequestHandler<GetAllExperienceQuery, List<ExperienceDTO>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetAllExperienceQueryHandler(IApplicationDbContext context, IMapper mapper)

    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ExperienceDTO>> Handle(GetAllExperienceQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Experience
            .Include(exp => exp.Employee.ApplicationUser)
            .Where(exp => exp.EmployeeId.Equals(request.Id))
            .OrderBy(exp => exp.EndDate)
            .ProjectTo<ExperienceDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }
}