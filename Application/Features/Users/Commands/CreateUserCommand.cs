using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Identity.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private IMapper _mapper;
        private readonly IUtility _utility;

        public CreateUserCommandHandler(IRepositoryAsync<User> repositoryAsync, IMapper mapper, IUtility utility)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            request.Password = _utility.EncryptSHA256(request.Password);
            var user = _mapper.Map<User>(request);
            var data = await _repositoryAsync.AddAsync(user);

            return new Response<int>(data.Id);
        }
    }
}