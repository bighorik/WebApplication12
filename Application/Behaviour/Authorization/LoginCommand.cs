using Application.Interfaces.Infrastructure;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Models;
using MediatR;
using Hasher = BCrypt.Net.BCrypt;

namespace Application.Behaviour.Authorization
{
    public class LoginCommand : IRequest<AccessPair>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler(
        IUserRepository userRepository,
        ITokenService tokenService) : IRequestHandler<LoginCommand, AccessPair>
    {
        public async Task<AccessPair?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetUserByLogin(request.Login);

            if (user == null && Hasher.Verify(request.Password, user.PasswordHash) == false)
                return null;
            ///Сохранить рефреш токен
            AccessPair pair = tokenService.GenerateTokens(user);

            return pair;
        }
    }
}
