using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Otus_19_authorization;

public class JwtService : IJwtService
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public JwtService(IUserService userService, IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
    }

    public JwtSecurityToken GenerateToken(string username, string gameId)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("gameId", gameId),
        };

        var allowedOperations = userService.GetAllowedOperations(username);
        foreach (string operation in allowedOperations)
        {
            claims.Add(new Claim("operation", operation));
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);
        
        return token;
    }
}
