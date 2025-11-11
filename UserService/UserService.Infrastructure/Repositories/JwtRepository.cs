using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Data;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class JwtRepository(UserManager<Person> userManager) : IJwtRepository
{
    public async Task<string> CreateJwtAsync(Person person)
    {
        var roles = await userManager.GetRolesAsync(person);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, person.Id),
            new(JwtRegisteredClaimNames.GivenName, person.UserName ?? string.Empty)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var jwt = new JwtSecurityToken(
            issuer: StaticData.ISSURE,
            audience: StaticData.AUDIENCE,
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticData.KEY)), SecurityAlgorithms.HmacSha256)
        );


        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}