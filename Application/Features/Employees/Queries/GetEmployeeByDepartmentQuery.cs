using Application.DTOs;
using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Queries
{
    public class GetEmployeeByDepartmentQuery : IRequest<Response<List<EmployeeDto>>>
    {
        public int DepartmentId { get; set; }
    }

    public class GetEmployeeByDepartmentQueryHandler : IRequestHandler<GetEmployeeByDepartmentQuery, Response<List<EmployeeDto>>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetEmployeeByDepartmentQueryHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<List<EmployeeDto>>> Handle(GetEmployeeByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repositoryAsync.ListAsync(new EmployeesSpecification(request.DepartmentId));
            
            if (employees == null || !employees.Any())
            {
                throw new KeyNotFoundException($"No employees found for department ID {request.DepartmentId}.");
            }
            else
            {
                var employeesDto = _mapper.Map<List<EmployeeDto>>(employees);
                return new Response<List<EmployeeDto>>(employeesDto);
            }
        }
    }
}
