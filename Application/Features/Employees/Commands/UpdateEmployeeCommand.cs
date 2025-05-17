using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Domain.Enumerators;
using MediatR;

namespace Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public JobPosition Position { get; set; }
        public int DepartmentId { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Employee> _repositoryAsync;

        public UpdateEmployeeCommandHandler(IRepositoryAsync<Employee> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repositoryAsync.GetByIdAsync(request.Id);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {request.Id} not found.");
            }
            else
            {
                employee.Name = request.Name;
                employee.Email = request.Email;
                employee.Salary = request.Salary;
                employee.Position = request.Position;
                employee.DepartmentId = request.DepartmentId;

                await _repositoryAsync.UpdateAsync(employee);

                return new Response<int>(employee.Id);
            }
        }
    }
}
