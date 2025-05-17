using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Queries
{
    public class GetAllEmployeesQuery : IRequest<Response<IEnumerable<EmployeeDto>>>
    {
    } 

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Response<IEnumerable<EmployeeDto>>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repositoryAsync.ListAsync();
            if (employees == null || !employees.Any())
            {
                throw new KeyNotFoundException("No employees found.");
            }
            else
            {
                var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                return new Response<IEnumerable<EmployeeDto>>(employeeDtos);
            }
        }
    }
}
