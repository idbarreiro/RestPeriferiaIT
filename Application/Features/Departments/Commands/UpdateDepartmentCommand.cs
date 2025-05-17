using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departments.Commands
{
    public class UpdateDepartmentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;

        public UpdateDepartmentCommandHandler(IRepositoryAsync<Department> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _repositoryAsync.GetByIdAsync(request.Id);

            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.Id} not found.");
            }
            else
            {
                department.Name = request.Name;
                
                await _repositoryAsync.UpdateAsync(department);

                return new Response<int>(department.Id);
            }
        }
    }
}
