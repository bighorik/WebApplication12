using Domain.Entities;
using Domain.Models;

namespace Application.Interfaces.Infrastructure
{
    public interface ITokenService
    {
        AccessPair GenerateTokens(UserEntity user);
    }
}
