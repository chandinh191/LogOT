using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogOT.Application.Common.Exceptions;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LogOT.Application.Employees.Commands.Update;
public record UploadCV : IRequest
{
    public Guid Id { get; set; }
    public IFormFile CVFile { get; set; }
    
}

public class UploadCVHandler : IRequestHandler<UploadCV>
{
    private readonly IApplicationDbContext _context;

    public UploadCVHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UploadCV request, CancellationToken cancellationToken)
    {
        string guidString = "ac69dc8e-f88d-46c2-a861-c9d5ac894141";

        // Using Guid.Parse
        Guid guid = Guid.Parse(guidString);

        // Using Guid.TryParse
        Guid.TryParse(guidString, out Guid parsedGuid);
        request.Id = parsedGuid;
        var employee = await _context.Employee.FindAsync(request.Id);

        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        var file = request.CVFile;
        if (file != null && file.Length > 0)
        {
            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Specify the directory to save the CV files
            var uploadDirectory = @"D:\Training_Ki_6\LogOT\CV";

            // Combine the directory and file name to get the full file path
            var filePath = Path.Combine(uploadDirectory, fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Update the CVPath property of the employee
            employee.CVPath = filePath;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}

