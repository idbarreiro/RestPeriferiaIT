using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Employees.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDto>>
    {
        public int Id { get; set; }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private readonly IMapper _mapper;
        public GetEmployeeByIdQueryHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repositoryAsync.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
            }
            else
            {
                var employeeDto = _mapper.Map<EmployeeDto>(employee);
                return new Response<EmployeeDto>(employeeDto);
            }
        }
    }
}
