using AuthService.Api.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Api.Authentication;
public class JwtTokenGenerator : ITokenGenerator
{
    private IConfiguration _configuration;
    public JwtTokenGenerator(IConfiguration configuration) => _configuration = configuration;

    public string Generate(string userName)
    {
        var jwt = _configuration.GetSection("Jwt").Get<JwtSettings>();

        if ((jwt?.Validate()) != true)
            throw new ArgumentNullException();

        else
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,userName),
                new Claim(JwtRegisteredClaimNames.Aud,jwt.Aud1),
                new Claim(JwtRegisteredClaimNames.Aud,jwt.Aud2),
                new Claim(JwtRegisteredClaimNames.Aud,jwt.Aud3),
            };
            var token = new JwtSecurityToken(jwt.Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
