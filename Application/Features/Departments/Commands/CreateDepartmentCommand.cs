using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = string.Empty;

    }
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;
        private IMapper _mapper;

        public CreateDepartmentCommandHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = _mapper.Map<Department>(request);
            var data = await _repositoryAsync.AddAsync(department);

            return new Response<int>(data.Id);
        }
    }
}
