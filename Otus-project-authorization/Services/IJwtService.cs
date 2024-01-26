using System.IdentityModel.Tokens.Jwt;

namespace Otus_project_authorization;

public interface IJwtService
{
    JwtSecurityToken GenerateToken(string username, string gameId);
}
