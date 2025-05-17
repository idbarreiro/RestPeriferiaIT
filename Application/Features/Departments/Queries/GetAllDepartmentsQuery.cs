using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<Response<IEnumerable<DepartmentDto>>>
    {
    }

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Response<IEnumerable<DepartmentDto>>>
    {
        private readonly IRepositoryAsync<Department> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IRepositoryAsync<Department> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _repositoryAsync.ListAsync();
            if (departments == null || !departments.Any())
            {
                throw new KeyNotFoundException("No departments found.");
            }
            else
            {
                var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
                return new Response<IEnumerable<DepartmentDto>>(departmentsDto);
            }
        }
    }
}
