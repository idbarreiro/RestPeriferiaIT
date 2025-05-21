using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Response<IEnumerable<UserDto>>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserDto>>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IRepositoryAsync<User> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repositoryAsync.ListAsync();
            if (users == null || !users.Any())
            {
                throw new KeyNotFoundException("No users found.");
            }
            else
            {
                var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
                return new Response<IEnumerable<UserDto>>(usersDto);
            }
        }
    }
}
