﻿using Microsoft.IdentityModel.Tokens;
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

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName,userName),
            new Claim(JwtRegisteredClaimNames.Aud,_configuration["Jwt:Aud1"]),
            new Claim(JwtRegisteredClaimNames.Aud,_configuration["Jwt:Aud2"]),
            new Claim(JwtRegisteredClaimNames.Aud,_configuration["Jwt:Aud3"]),

        };
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            null,
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
