using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;

        public DeleteDepartmentCommandHandler(IRepositoryAsync<Department> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _repositoryAsync.GetByIdAsync(request.Id);

            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.Id} not found.");
            }
            else
            {
                await _repositoryAsync.DeleteAsync(department);

                return new Response<int>(department.Id);
            }
        }
    }
}
