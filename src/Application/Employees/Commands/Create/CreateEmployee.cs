using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.IdentityModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees.Commands.Create
{
    public record CreateEmployee : IRequest<string>
    {
        public string? ApplicationUserId { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDay { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankName { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployee, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateEmployee request, CancellationToken cancellationToken)
        {
            
            var entity = new Employee
            {
                ApplicationUserId = request.ApplicationUserId,
                IdentityNumber = request.IdentityNumber,
                CreatedBy = "2",
                LastModifiedBy = "1",
                BirthDay = request.BirthDay,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                BankName = request.BankName,
                BankAccountNumber = request.BankAccountNumber,
                BankAccountName = request.BankAccountName,
            };

            _context.Employee.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.IdentityNumber;
        }
    }
}
