using Application.Interfaces;
using Application.Specifications;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Identity.Interfaces;
using MediatR;

namespace Application.Features.Login.Queries
{
    public class GetUserByEmail : IRequest<Response<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmail, Response<string>>
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly IUtility _utility;

        public GetUserByEmailHandler(IRepositoryAsync<User> repositoryAsync, IMapper mapper, IUtility utility)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _utility = utility;
        }

        public async Task<Response<string>> Handle(GetUserByEmail request, CancellationToken cancellationToken)
        {
            var user = await _repositoryAsync.FirstOrDefaultAsync(new UsersSpecification(request.Email));
            if (user == null || !user.IsActive)
            {
                throw new KeyNotFoundException($"User with email {request.Email} not found or inactive.");
            }
            else if (user.Password != _utility.EncryptSHA256(request.Password))
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }
            else
            {
                user.Password = _utility.EncryptSHA256(request.Password);
                var token = _utility.GenerateJwtToken(user);

                return new Response<string>(token, "");
            }
        }
    }
}
