using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enumerators;
using MediatR;

namespace Application.Features.Departments.Queries
{
    public class GetDepartmentSalaryQuery : IRequest<Response<decimal>>
    {
        public int DepartmentId { get; set; }
    }

    public class GetDepartmentSalaryQueryHandler : IRequestHandler<GetDepartmentSalaryQuery, Response<decimal>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;

        public GetDepartmentSalaryQueryHandler(IRepositoryAsync<Employee> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<decimal>> Handle(GetDepartmentSalaryQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repositoryAsync.ListAsync(new EmployeesSpecification(request.DepartmentId));

            if (employees == null || !employees.Any())
            {
                throw new KeyNotFoundException($"No employees found for calculate salary in the department ID {request.DepartmentId}.");
            }
            else
            {
                var totalSalary = employees.Sum(e => e.Position switch
                {
                    JobPosition.Manager => e.Salary * 1.20m,
                    JobPosition.Developer => e.Salary * 1.10m,
                    _ => e.Salary
                });

                return new Response<decimal>(totalSalary);
            }
        }
    }
}
