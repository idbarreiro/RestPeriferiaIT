using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Enumerators;
using MediatR;

namespace Application.Features.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public JobPosition Position { get; set; }
        public int DepartmentId { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;
        private IMapper _mapper;

        public CreateEmployeeCommandHandler(IRepositoryAsync<Employee> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);
            var data = await _repositoryAsync.AddAsync(employee);

            return new Response<int>(data.Id);
        }
    }
}
