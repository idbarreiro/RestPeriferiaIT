using Application.DTOs;
using Application.Features.Employees.Queries;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<Response<DepartmentDto>>
    {
        public int Id { get; set; }
    }

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Response<DepartmentDto>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }
        
        public async Task<Response<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _repositoryAsync.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {request.Id} not found.");
            }
            else
            {
                var departmentDto = _mapper.Map<DepartmentDto>(department);
                return new Response<DepartmentDto>(departmentDto);
            }
        }
    }
}
